﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMDataAccess.Scripts {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SqlScripts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SqlScripts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SMDataAccess.Scripts.SqlScripts", typeof(SqlScripts).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO TbPart (PartName, Price, Notes, UpdateDate, CreateDate)
        ///VALUES (@PartName, @Price, @Notes, GETDATE(), GETDATE())
        ///
        ///DECLARE @PartId INT = SCOPE_IDENTITY()
        ///
        ///SELECT p.PartId, p.PartName, p.Price, p.Notes, p.UpdateDate, p.CreateDate, p.IsDeleted FROM TbPart as p
        ///WHERE p.PartId = @PartId
        ///
        ///SELECT InventoryId, inv.PartId, QuantityAvailable, PONumber, UpdateDate, CreateDate FROM TbInventory as inv
        ///WHERE inv.PartId = @PartId.
        /// </summary>
        internal static string PartAdd {
            get {
                return ResourceManager.GetString("PartAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE TbPart
        ///SET IsDeleted = 1
        ///WHERE PartId = @PartId.
        /// </summary>
        internal static string PartDelete {
            get {
                return ResourceManager.GetString("PartDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT p.PartId, p.PartName, p.Price, p.Notes, p.UpdateDate, p.CreateDate, p.IsDeleted FROM TbPart as p
        ///WHERE p.PartId = @PartId
        ///
        ///SELECT InventoryId, inv.PartId, QuantityAvailable, PONumber, UpdateDate, CreateDate FROM TbInventory as inv
        ///WHERE inv.PartId = @PartId.
        /// </summary>
        internal static string PartGetById {
            get {
                return ResourceManager.GetString("PartGetById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF OBJECT_ID(&apos;tempdb..#PartSelect&apos;) IS NOT NULL DROP TABLE #PartSelect
        ///
        ///SELECT COUNT(*) AS TotalCount FROM TbPart
        ////*@QueryFilters@*/
        ///
        ///;WITH AvailableParts AS (
        ///SELECT ROW_NUMBER() OVER (ORDER BY PartId) AS RowNumber, p.PartId FROM TbPart as p
        ////*@QueryFilters@*/
        ///)
        ///
        ///SELECT PartId 
        ///INTO #PartSelect
        ///FROM AvailableParts
        ///WHERE RowNumber BETWEEN (@PageNumber-1)*@PageSize+1 AND (@PageNumber)*@PageSize
        ///
        ///SELECT p.PartId, p.PartName, p.Price, p.Notes, p.UpdateDate, p.CreateDate, p.IsDeleted FROM TbPart  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string PartGetPaginated {
            get {
                return ResourceManager.GetString("PartGetPaginated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE TbPart
        ///SET UpdateDate = GETDATE(), PartName = @PartName, Price = @Price, Notes = @Notes
        ///WHERE PartId = @PartId 
        ///
        ///DECLARE @PartId INT = SCOPE_IDENTITY()
        ///
        ///SELECT p.PartId, p.PartName, p.Price, p.Notes, p.UpdateDate, p.CreateDate, p.IsDeleted FROM TbPart as p
        ///WHERE p.PartId = @PartId
        ///
        ///SELECT InventoryId, inv.PartId, QuantityAvailable, PONumber, UpdateDate, CreateDate FROM TbInventory as inv
        ///WHERE inv.PartId = @PartId.
        /// </summary>
        internal static string PartUpdate {
            get {
                return ResourceManager.GetString("PartUpdate", resourceCulture);
            }
        }
    }
}
