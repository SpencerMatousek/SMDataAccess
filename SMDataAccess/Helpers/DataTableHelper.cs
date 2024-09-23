using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDataAccess.Helpers;
public class DataTableHelper<T> where T : class
{
    private DataTable? _dataTable;

    /// <summary>
    /// Creates an empty DataTable from the specified type
    /// </summary>
    public void CreateDTFromObject()
    {
        _dataTable = new();
        Type objectType = typeof(T);

        var properties = objectType.GetProperties();

        // Create columns on DataTable
        foreach (var property in properties)
        {
            try
            {
                DataColumn column = new DataColumn();
                Type propertyType = property.PropertyType;
                if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                {
                    column.AllowDBNull = true;
                    propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? throw new ArgumentNullException();
                }
                column.ColumnName = property.Name;
                column.DataType = propertyType;

                _dataTable.Columns.Add(column);
            }
            catch (Exception)
            {
                throw;
            }
        }
        return;
    }

    /// <summary>
    /// Populates the DataTable with data.
    /// </summary>
    /// <param name="data">Data to populate the DataTable with</param>
    /// <returns>Populated DataTable</returns>
    public DataTable PopulateDTFromObject(List<T> data)
    {
        var properties = typeof(T).GetProperties();
        if (_dataTable != null)
        {
            foreach (var obj in data)
            {
                DataRow row = _dataTable.NewRow();
                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(obj) ?? DBNull.Value;
                }
                _dataTable.Rows.Add(row);
            }
        }
        return _dataTable;
    }

    /// <summary>
    /// Converts a List of specified type to a DataTable
    /// </summary>
    /// <param name="data">Data to be added to be initialized in the DataTable</param>
    /// <returns>DataTable populated with data</returns>
    public DataTable ConvertToDt(List<T> data)
    {
        CreateDTFromObject();
        return PopulateDTFromObject(data);
    }
}
