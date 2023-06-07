SELECT GroupName , sum(Payments.Amount) as 'ясллю дкъ цпсоо хрх'
FROM Student, Payments
WHERE GroupName = 'ITI'
GROUP BY GroupName 