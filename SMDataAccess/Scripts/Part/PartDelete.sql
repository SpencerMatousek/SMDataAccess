IF NOT EXISTS (SELECT * FROM TbPart WHERE PartId = @PartId AND IsDeleted = 0)
THROW 51000, 'No record is found with that ID that is not already deleted', 1;

UPDATE TbPart
SET IsDeleted = 1
WHERE PartId = @PartId