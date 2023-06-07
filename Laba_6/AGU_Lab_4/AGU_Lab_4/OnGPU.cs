using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGU_Lab_4
{
    public class OnGPU : GameWindow
    {

        public float[,] CalculaterOnGPU()
        {
            GameWindow gameWindow = new GameWindow(800, 600, GraphicsMode.Default, "", GameWindowFlags.Default, DisplayDevice.Default, 4, 4, GraphicsContextFlags.Debug);
            gameWindow.MakeCurrent();
            gameWindow.Visible = false;
            // Задаем размеры матриц
            int N = 3;
            int M = 3;

            // Создаем матрицы
            float[,] matrix1 = new float[N, M];
            float[,] matrix2 = new float[N, M];
            float scalar = 2.0f;

            // Заполняем матрицы случайными значениями
            Random random = new Random();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    matrix1[i, j] = (float)random.NextDouble();
                    matrix2[i, j] = (float)random.NextDouble();
                }
            }

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

            // Освобождаем ресурсы
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteProgram(program);
            GL.DeleteBuffer(matrix1Buffer);
            GL.DeleteBuffer(matrix2Buffer);
            GL.DeleteBuffer(vertexBuffer);

            return result;
        }
    }
}
