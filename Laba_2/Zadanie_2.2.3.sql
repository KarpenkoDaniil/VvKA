SELECT GroupName , sum(Payments.Amount) as '����� ��� ����� ���'
FROM Student, Payments
WHERE GroupName = 'ITI'
GROUP BY GroupName 