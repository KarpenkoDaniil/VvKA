SELECT        Streets.Name, Town.Names
FROM            Town INNER JOIN
                         Streets ON Town.TownsId = Streets.TownsId
WHERE        (Town.Names = 'Гомель')
