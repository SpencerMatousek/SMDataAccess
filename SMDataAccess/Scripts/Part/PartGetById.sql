SELECT p.PartId, p.PartName, p.Price, p.Notes, p.UpdateDate, p.CreateDate, p.IsDeleted FROM TbPart as p
WHERE p.PartId = @PartId

SELECT InventoryId, inv.PartId, QuantityAvailable, PONumber, UpdateDate, CreateDate FROM TbInventory as inv
WHERE inv.PartId = @PartId