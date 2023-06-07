using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace AGU_Lab_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Random random = new Random();

            // Задаем размеры матриц
            int N = 6;
            int M = 6;

            // Создаем матрицы
            matrix1 = new float[N, M];
            matrix2 = new float[N, M];

            // Заполняем матрицы случайными значениями
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    matrix1[i, j] = (float)random.NextDouble();
                    matrix2[i, j] = (float)random.NextDouble();
                }
            }
        }
        float[,] matrix1;
        float[,] matrix2;
        private void CalculateCPU_Click(object sender, EventArgs e)
        {
            // Задаем размеры матриц
            int N = 6;
            int M = 6;
            float scalar = 2f;


            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <M; j++)
                {
                    matrix1[i,j] = matrix1[i, j] * scalar + matrix2[i,j];
                }
            }

            ResultCPU.Text = ResultCPU.Text + "\n";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (j == M - 1)
                    {
                        ResultCPU.Text = ResultCPU.Text + "  " + matrix1[i, j] + "\n";
                    }
                    else
                    {
                        ResultCPU.Text = ResultCPU.Text + "  " + matrix1[i, j];
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameWindow gameWindow = new GameWindow(800, 600, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 4, 4, GraphicsContextFlags.Debug);
            gameWindow.MakeCurrent();
            gameWindow.Visible = false;

            // Задаем размеры матриц
            int N = 6;
            int M = 6;

            // Создаем матрицы

            float scalar = 2.0f;

            // Создаем шейдерную программу
            string vertexShaderSource = @"
#version 330

layout(location = 0) in vec3 vertexPosition;

void main()
{
    gl_Position = vec4(vertexPosition, 1.0);
}
";

            string fragmentShaderSource = @"
#version 330

uniform mat3 matrix1;
uniform mat3 matrix2;
uniform float scalar;

out vec4 fragColor;

void main()
{
    mat3 result = matrix1 * scalar + matrix2;
    fragColor = vec4(result[0], result[1], result[2], 1.0);
}
";

            int program = GL.CreateProgram();

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);

            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);

            GL.LinkProgram(program);

            // Создаем буферы для хранения данных матриц на GPU
            int matrix1Buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.UniformBuffer, matrix1Buffer);
            GL.BufferData(BufferTarget.UniformBuffer, sizeof(float) * N * M, matrix1, BufferUsageHint.StaticDraw);

            int matrix2Buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.UniformBuffer, matrix2Buffer);
            GL.BufferData(BufferTarget.UniformBuffer, sizeof(float) * N * M, matrix2, BufferUsageHint.StaticDraw);

            // Связываем буферы с шейдерной программой
            int matrix1Location = GL.GetUniformBlockIndex(program, "matrix1");
            int matrix2Location = GL.GetUniformBlockIndex(program, "matrix2");

            GL.UniformBlockBinding(program, matrix1Location, 0);
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, 0, matrix1Buffer);

            GL.UniformBlockBinding(program, matrix2Location, 1);
            GL.BindBufferBase(BufferRangeTarget.UniformBuffer, 1, matrix2Buffer);

            // Устанавливаем значение скаляра
            int scalarLocation = GL.GetUniformLocation(program, "scalar");
            GL.Uniform1(scalarLocation, scalar);

            // Создаем вершинный буфер
            float[] vertices = new float[] { -1.0f, -1.0f, 0.0f, 1.0f, 3.0f, -1.0f, 0.0f, 1.0f, -1.0f, 3.0f, 0.0f, 1.0f };
            int vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Задаем формат вершинного буфера
            int vertexLocation = GL.GetAttribLocation(program, "vertexPosition");
            GL.VertexAttribPointer(vertexLocation, 4, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(vertexLocation);

            // Рендерим треугольник
            GL.UseProgram(program);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            // Получаем результаты операций из буфера на GPU и сохраняем их в матрицу на CPU
            float[,] result = new float[N, M];
            GL.BindBuffer(BufferTarget.UniformBuffer, matrix1Buffer);
            GL.GetBufferSubData(BufferTarget.UniformBuffer, IntPtr.Zero, sizeof(float) * N * M, result);

            ResultGPU.Text = ResultCPU.Text + "\n";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (j == M - 1)
                    {
                        ResultGPU.Text = ResultGPU.Text + "  " + result[i, j] + "\n";
                    }
                    else
                    {
                        ResultGPU.Text = ResultGPU.Text + "  " + result[i, j];
                    }
                }
            }

            // Освобождаем ресурсы
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteProgram(program);
            GL.DeleteBuffer(matrix1Buffer);
            GL.DeleteBuffer(matrix2Buffer);
            GL.DeleteBuffer(vertexBuffer);
        }
    }
}
