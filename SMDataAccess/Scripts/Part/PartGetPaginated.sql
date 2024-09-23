IF OBJECT_ID('tempdb..#PartSelect') IS NOT NULL DROP TABLE #PartSelect

SELECT COUNT(*) AS TotalCount FROM TbPart
/*@QueryFilters@*/

;WITH AvailableParts AS (
SELECT ROW_NUMBER() OVER (/*@OrderParams@*/) AS RowNumber, p.PartId FROM TbPart as p
/*@QueryFilters@*/
)

SELECT PartId 
INTO #PartSelect
FROM AvailableParts
WHERE RowNumber BETWEEN (@PageNumber-1)*@PageSize+1 AND (@PageNumber)*@PageSize

SELECT p.PartId, p.PartName, p.Price, p.Notes, p.UpdateDate, p.CreateDate, p.IsDeleted FROM TbPart as p
	INNER JOIN #PartSelect as ps ON p.PartId = ps.PartId

SELECT InventoryId, inv.PartId, QuantityAvailable, PONumber, UpdateDate, CreateDate FROM TbInventory as inv
	INNER JOIN #PartSelect AS PS on ps.PartId = inv.PartId

DROP TABLE #PartSelect