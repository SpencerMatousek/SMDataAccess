using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDataAccess.Models;
public class TbInventory
{
    public int InventoryId { get; set; }
    public int PartId { get; set; }
    public int QuantityAvailable { get; set; }
    public string? PONumber { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime CreateDate { get; set; }
}
