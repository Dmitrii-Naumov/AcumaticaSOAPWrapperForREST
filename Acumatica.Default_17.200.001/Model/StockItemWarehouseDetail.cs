/* 
 * Default/17.200.001
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 3
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Acumatica.RESTClient.Model;
using System.ComponentModel.DataAnnotations;


namespace Acumatica.Default_17_200_001.Model
{
    /// <summary>
    /// StockItemWarehouseDetail
    /// </summary>
    [DataContract]
    public partial class StockItemWarehouseDetail : Entity,  IEquatable<StockItemWarehouseDetail>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockItemWarehouseDetail" /> class.
        /// </summary>
        /// <param name="dailyDemandForecast">dailyDemandForecast.</param>
        /// <param name="dailyDemandForecastErrorSTDEV">dailyDemandForecastErrorSTDEV.</param>
        /// <param name="defaultIssueLocationID">defaultIssueLocationID.</param>
        /// <param name="defaultReceiptLocationID">defaultReceiptLocationID.</param>
        /// <param name="inventoryAccount">inventoryAccount.</param>
        /// <param name="inventorySubaccount">inventorySubaccount.</param>
        /// <param name="isDefault">isDefault.</param>
        /// <param name="lastForecastDate">lastForecastDate.</param>
        /// <param name="_override">_override.</param>
        /// <param name="overridePreferredVendor">overridePreferredVendor.</param>
        /// <param name="overrideReplenishmentSettings">overrideReplenishmentSettings.</param>
        /// <param name="overrideStdCost">overrideStdCost.</param>
        /// <param name="preferredVendor">preferredVendor.</param>
        /// <param name="priceOverride">priceOverride.</param>
        /// <param name="productManager">productManager.</param>
        /// <param name="productWorkgroup">productWorkgroup.</param>
        /// <param name="qtyOnHand">qtyOnHand.</param>
        /// <param name="replenishmentSource">replenishmentSource.</param>
        /// <param name="replenishmentWarehouse">replenishmentWarehouse.</param>
        /// <param name="seasonality">seasonality.</param>
        /// <param name="serviceLevel">serviceLevel.</param>
        /// <param name="status">status.</param>
        /// <param name="warehouseID">warehouseID.</param>
        public StockItemWarehouseDetail(DecimalValue dailyDemandForecast = default(DecimalValue), DecimalValue dailyDemandForecastErrorSTDEV = default(DecimalValue), StringValue defaultIssueLocationID = default(StringValue), StringValue defaultReceiptLocationID = default(StringValue), StringValue inventoryAccount = default(StringValue), StringValue inventorySubaccount = default(StringValue), BooleanValue isDefault = default(BooleanValue), DateTimeValue lastForecastDate = default(DateTimeValue), BooleanValue _override = default(BooleanValue), BooleanValue overridePreferredVendor = default(BooleanValue), BooleanValue overrideReplenishmentSettings = default(BooleanValue), BooleanValue overrideStdCost = default(BooleanValue), StringValue preferredVendor = default(StringValue), BooleanValue priceOverride = default(BooleanValue), StringValue productManager = default(StringValue), StringValue productWorkgroup = default(StringValue), DecimalValue qtyOnHand = default(DecimalValue), StringValue replenishmentSource = default(StringValue), StringValue replenishmentWarehouse = default(StringValue), StringValue seasonality = default(StringValue), DecimalValue serviceLevel = default(DecimalValue), StringValue status = default(StringValue), StringValue warehouseID = default(StringValue), Guid? id = default(Guid?), long? rowNumber = default(long?), string note = default(string), Dictionary<string, Dictionary<string, CustomField>> custom = default(Dictionary<string, Dictionary<string, CustomField>>), List<FileLink> files = default(List<FileLink>)) : base(id, rowNumber, note, custom, files)
        {
            this.DailyDemandForecast = dailyDemandForecast;
            this.DailyDemandForecastErrorSTDEV = dailyDemandForecastErrorSTDEV;
            this.DefaultIssueLocationID = defaultIssueLocationID;
            this.DefaultReceiptLocationID = defaultReceiptLocationID;
            this.InventoryAccount = inventoryAccount;
            this.InventorySubaccount = inventorySubaccount;
            this.IsDefault = isDefault;
            this.LastForecastDate = lastForecastDate;
            this.Override = _override;
            this.OverridePreferredVendor = overridePreferredVendor;
            this.OverrideReplenishmentSettings = overrideReplenishmentSettings;
            this.OverrideStdCost = overrideStdCost;
            this.PreferredVendor = preferredVendor;
            this.PriceOverride = priceOverride;
            this.ProductManager = productManager;
            this.ProductWorkgroup = productWorkgroup;
            this.QtyOnHand = qtyOnHand;
            this.ReplenishmentSource = replenishmentSource;
            this.ReplenishmentWarehouse = replenishmentWarehouse;
            this.Seasonality = seasonality;
            this.ServiceLevel = serviceLevel;
            this.Status = status;
            this.WarehouseID = warehouseID;
        }
        
        /// <summary>
        /// Gets or Sets DailyDemandForecast
        /// </summary>
        [DataMember(Name="DailyDemandForecast", EmitDefaultValue=false)]
        public DecimalValue DailyDemandForecast { get; set; }

        /// <summary>
        /// Gets or Sets DailyDemandForecastErrorSTDEV
        /// </summary>
        [DataMember(Name="DailyDemandForecastErrorSTDEV", EmitDefaultValue=false)]
        public DecimalValue DailyDemandForecastErrorSTDEV { get; set; }

        /// <summary>
        /// Gets or Sets DefaultIssueLocationID
        /// </summary>
        [DataMember(Name="DefaultIssueLocationID", EmitDefaultValue=false)]
        public StringValue DefaultIssueLocationID { get; set; }

        /// <summary>
        /// Gets or Sets DefaultReceiptLocationID
        /// </summary>
        [DataMember(Name="DefaultReceiptLocationID", EmitDefaultValue=false)]
        public StringValue DefaultReceiptLocationID { get; set; }

        /// <summary>
        /// Gets or Sets InventoryAccount
        /// </summary>
        [DataMember(Name="InventoryAccount", EmitDefaultValue=false)]
        public StringValue InventoryAccount { get; set; }

        /// <summary>
        /// Gets or Sets InventorySubaccount
        /// </summary>
        [DataMember(Name="InventorySubaccount", EmitDefaultValue=false)]
        public StringValue InventorySubaccount { get; set; }

        /// <summary>
        /// Gets or Sets IsDefault
        /// </summary>
        [DataMember(Name="IsDefault", EmitDefaultValue=false)]
        public BooleanValue IsDefault { get; set; }

        /// <summary>
        /// Gets or Sets LastForecastDate
        /// </summary>
        [DataMember(Name="LastForecastDate", EmitDefaultValue=false)]
        public DateTimeValue LastForecastDate { get; set; }

        /// <summary>
        /// Gets or Sets Override
        /// </summary>
        [DataMember(Name="Override", EmitDefaultValue=false)]
        public BooleanValue Override { get; set; }

        /// <summary>
        /// Gets or Sets OverridePreferredVendor
        /// </summary>
        [DataMember(Name="OverridePreferredVendor", EmitDefaultValue=false)]
        public BooleanValue OverridePreferredVendor { get; set; }

        /// <summary>
        /// Gets or Sets OverrideReplenishmentSettings
        /// </summary>
        [DataMember(Name="OverrideReplenishmentSettings", EmitDefaultValue=false)]
        public BooleanValue OverrideReplenishmentSettings { get; set; }

        /// <summary>
        /// Gets or Sets OverrideStdCost
        /// </summary>
        [DataMember(Name="OverrideStdCost", EmitDefaultValue=false)]
        public BooleanValue OverrideStdCost { get; set; }

        /// <summary>
        /// Gets or Sets PreferredVendor
        /// </summary>
        [DataMember(Name="PreferredVendor", EmitDefaultValue=false)]
        public StringValue PreferredVendor { get; set; }

        /// <summary>
        /// Gets or Sets PriceOverride
        /// </summary>
        [DataMember(Name="PriceOverride", EmitDefaultValue=false)]
        public BooleanValue PriceOverride { get; set; }

        /// <summary>
        /// Gets or Sets ProductManager
        /// </summary>
        [DataMember(Name="ProductManager", EmitDefaultValue=false)]
        public StringValue ProductManager { get; set; }

        /// <summary>
        /// Gets or Sets ProductWorkgroup
        /// </summary>
        [DataMember(Name="ProductWorkgroup", EmitDefaultValue=false)]
        public StringValue ProductWorkgroup { get; set; }

        /// <summary>
        /// Gets or Sets QtyOnHand
        /// </summary>
        [DataMember(Name="QtyOnHand", EmitDefaultValue=false)]
        public DecimalValue QtyOnHand { get; set; }

        /// <summary>
        /// Gets or Sets ReplenishmentSource
        /// </summary>
        [DataMember(Name="ReplenishmentSource", EmitDefaultValue=false)]
        public StringValue ReplenishmentSource { get; set; }

        /// <summary>
        /// Gets or Sets ReplenishmentWarehouse
        /// </summary>
        [DataMember(Name="ReplenishmentWarehouse", EmitDefaultValue=false)]
        public StringValue ReplenishmentWarehouse { get; set; }

        /// <summary>
        /// Gets or Sets Seasonality
        /// </summary>
        [DataMember(Name="Seasonality", EmitDefaultValue=false)]
        public StringValue Seasonality { get; set; }

        /// <summary>
        /// Gets or Sets ServiceLevel
        /// </summary>
        [DataMember(Name="ServiceLevel", EmitDefaultValue=false)]
        public DecimalValue ServiceLevel { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name="Status", EmitDefaultValue=false)]
        public StringValue Status { get; set; }

        /// <summary>
        /// Gets or Sets WarehouseID
        /// </summary>
        [DataMember(Name="WarehouseID", EmitDefaultValue=false)]
        public StringValue WarehouseID { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class StockItemWarehouseDetail {\n");
            sb.Append("  ").Append(base.ToString().Replace("\n", "\n  ")).Append("\n");
            sb.Append("  DailyDemandForecast: ").Append(DailyDemandForecast).Append("\n");
            sb.Append("  DailyDemandForecastErrorSTDEV: ").Append(DailyDemandForecastErrorSTDEV).Append("\n");
            sb.Append("  DefaultIssueLocationID: ").Append(DefaultIssueLocationID).Append("\n");
            sb.Append("  DefaultReceiptLocationID: ").Append(DefaultReceiptLocationID).Append("\n");
            sb.Append("  InventoryAccount: ").Append(InventoryAccount).Append("\n");
            sb.Append("  InventorySubaccount: ").Append(InventorySubaccount).Append("\n");
            sb.Append("  IsDefault: ").Append(IsDefault).Append("\n");
            sb.Append("  LastForecastDate: ").Append(LastForecastDate).Append("\n");
            sb.Append("  Override: ").Append(Override).Append("\n");
            sb.Append("  OverridePreferredVendor: ").Append(OverridePreferredVendor).Append("\n");
            sb.Append("  OverrideReplenishmentSettings: ").Append(OverrideReplenishmentSettings).Append("\n");
            sb.Append("  OverrideStdCost: ").Append(OverrideStdCost).Append("\n");
            sb.Append("  PreferredVendor: ").Append(PreferredVendor).Append("\n");
            sb.Append("  PriceOverride: ").Append(PriceOverride).Append("\n");
            sb.Append("  ProductManager: ").Append(ProductManager).Append("\n");
            sb.Append("  ProductWorkgroup: ").Append(ProductWorkgroup).Append("\n");
            sb.Append("  QtyOnHand: ").Append(QtyOnHand).Append("\n");
            sb.Append("  ReplenishmentSource: ").Append(ReplenishmentSource).Append("\n");
            sb.Append("  ReplenishmentWarehouse: ").Append(ReplenishmentWarehouse).Append("\n");
            sb.Append("  Seasonality: ").Append(Seasonality).Append("\n");
            sb.Append("  ServiceLevel: ").Append(ServiceLevel).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  WarehouseID: ").Append(WarehouseID).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as StockItemWarehouseDetail);
        }

        /// <summary>
        /// Returns true if StockItemWarehouseDetail instances are equal
        /// </summary>
        /// <param name="input">Instance of StockItemWarehouseDetail to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StockItemWarehouseDetail input)
        {
            if (input == null)
                return false;

            return base.Equals(input) && 
                (
                    this.DailyDemandForecast == input.DailyDemandForecast ||
                    (this.DailyDemandForecast != null &&
                    this.DailyDemandForecast.Equals(input.DailyDemandForecast))
                ) && base.Equals(input) && 
                (
                    this.DailyDemandForecastErrorSTDEV == input.DailyDemandForecastErrorSTDEV ||
                    (this.DailyDemandForecastErrorSTDEV != null &&
                    this.DailyDemandForecastErrorSTDEV.Equals(input.DailyDemandForecastErrorSTDEV))
                ) && base.Equals(input) && 
                (
                    this.DefaultIssueLocationID == input.DefaultIssueLocationID ||
                    (this.DefaultIssueLocationID != null &&
                    this.DefaultIssueLocationID.Equals(input.DefaultIssueLocationID))
                ) && base.Equals(input) && 
                (
                    this.DefaultReceiptLocationID == input.DefaultReceiptLocationID ||
                    (this.DefaultReceiptLocationID != null &&
                    this.DefaultReceiptLocationID.Equals(input.DefaultReceiptLocationID))
                ) && base.Equals(input) && 
                (
                    this.InventoryAccount == input.InventoryAccount ||
                    (this.InventoryAccount != null &&
                    this.InventoryAccount.Equals(input.InventoryAccount))
                ) && base.Equals(input) && 
                (
                    this.InventorySubaccount == input.InventorySubaccount ||
                    (this.InventorySubaccount != null &&
                    this.InventorySubaccount.Equals(input.InventorySubaccount))
                ) && base.Equals(input) && 
                (
                    this.IsDefault == input.IsDefault ||
                    (this.IsDefault != null &&
                    this.IsDefault.Equals(input.IsDefault))
                ) && base.Equals(input) && 
                (
                    this.LastForecastDate == input.LastForecastDate ||
                    (this.LastForecastDate != null &&
                    this.LastForecastDate.Equals(input.LastForecastDate))
                ) && base.Equals(input) && 
                (
                    this.Override == input.Override ||
                    (this.Override != null &&
                    this.Override.Equals(input.Override))
                ) && base.Equals(input) && 
                (
                    this.OverridePreferredVendor == input.OverridePreferredVendor ||
                    (this.OverridePreferredVendor != null &&
                    this.OverridePreferredVendor.Equals(input.OverridePreferredVendor))
                ) && base.Equals(input) && 
                (
                    this.OverrideReplenishmentSettings == input.OverrideReplenishmentSettings ||
                    (this.OverrideReplenishmentSettings != null &&
                    this.OverrideReplenishmentSettings.Equals(input.OverrideReplenishmentSettings))
                ) && base.Equals(input) && 
                (
                    this.OverrideStdCost == input.OverrideStdCost ||
                    (this.OverrideStdCost != null &&
                    this.OverrideStdCost.Equals(input.OverrideStdCost))
                ) && base.Equals(input) && 
                (
                    this.PreferredVendor == input.PreferredVendor ||
                    (this.PreferredVendor != null &&
                    this.PreferredVendor.Equals(input.PreferredVendor))
                ) && base.Equals(input) && 
                (
                    this.PriceOverride == input.PriceOverride ||
                    (this.PriceOverride != null &&
                    this.PriceOverride.Equals(input.PriceOverride))
                ) && base.Equals(input) && 
                (
                    this.ProductManager == input.ProductManager ||
                    (this.ProductManager != null &&
                    this.ProductManager.Equals(input.ProductManager))
                ) && base.Equals(input) && 
                (
                    this.ProductWorkgroup == input.ProductWorkgroup ||
                    (this.ProductWorkgroup != null &&
                    this.ProductWorkgroup.Equals(input.ProductWorkgroup))
                ) && base.Equals(input) && 
                (
                    this.QtyOnHand == input.QtyOnHand ||
                    (this.QtyOnHand != null &&
                    this.QtyOnHand.Equals(input.QtyOnHand))
                ) && base.Equals(input) && 
                (
                    this.ReplenishmentSource == input.ReplenishmentSource ||
                    (this.ReplenishmentSource != null &&
                    this.ReplenishmentSource.Equals(input.ReplenishmentSource))
                ) && base.Equals(input) && 
                (
                    this.ReplenishmentWarehouse == input.ReplenishmentWarehouse ||
                    (this.ReplenishmentWarehouse != null &&
                    this.ReplenishmentWarehouse.Equals(input.ReplenishmentWarehouse))
                ) && base.Equals(input) && 
                (
                    this.Seasonality == input.Seasonality ||
                    (this.Seasonality != null &&
                    this.Seasonality.Equals(input.Seasonality))
                ) && base.Equals(input) && 
                (
                    this.ServiceLevel == input.ServiceLevel ||
                    (this.ServiceLevel != null &&
                    this.ServiceLevel.Equals(input.ServiceLevel))
                ) && base.Equals(input) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && base.Equals(input) && 
                (
                    this.WarehouseID == input.WarehouseID ||
                    (this.WarehouseID != null &&
                    this.WarehouseID.Equals(input.WarehouseID))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = base.GetHashCode();
                if (this.DailyDemandForecast != null)
                    hashCode = hashCode * 59 + this.DailyDemandForecast.GetHashCode();
                if (this.DailyDemandForecastErrorSTDEV != null)
                    hashCode = hashCode * 59 + this.DailyDemandForecastErrorSTDEV.GetHashCode();
                if (this.DefaultIssueLocationID != null)
                    hashCode = hashCode * 59 + this.DefaultIssueLocationID.GetHashCode();
                if (this.DefaultReceiptLocationID != null)
                    hashCode = hashCode * 59 + this.DefaultReceiptLocationID.GetHashCode();
                if (this.InventoryAccount != null)
                    hashCode = hashCode * 59 + this.InventoryAccount.GetHashCode();
                if (this.InventorySubaccount != null)
                    hashCode = hashCode * 59 + this.InventorySubaccount.GetHashCode();
                if (this.IsDefault != null)
                    hashCode = hashCode * 59 + this.IsDefault.GetHashCode();
                if (this.LastForecastDate != null)
                    hashCode = hashCode * 59 + this.LastForecastDate.GetHashCode();
                if (this.Override != null)
                    hashCode = hashCode * 59 + this.Override.GetHashCode();
                if (this.OverridePreferredVendor != null)
                    hashCode = hashCode * 59 + this.OverridePreferredVendor.GetHashCode();
                if (this.OverrideReplenishmentSettings != null)
                    hashCode = hashCode * 59 + this.OverrideReplenishmentSettings.GetHashCode();
                if (this.OverrideStdCost != null)
                    hashCode = hashCode * 59 + this.OverrideStdCost.GetHashCode();
                if (this.PreferredVendor != null)
                    hashCode = hashCode * 59 + this.PreferredVendor.GetHashCode();
                if (this.PriceOverride != null)
                    hashCode = hashCode * 59 + this.PriceOverride.GetHashCode();
                if (this.ProductManager != null)
                    hashCode = hashCode * 59 + this.ProductManager.GetHashCode();
                if (this.ProductWorkgroup != null)
                    hashCode = hashCode * 59 + this.ProductWorkgroup.GetHashCode();
                if (this.QtyOnHand != null)
                    hashCode = hashCode * 59 + this.QtyOnHand.GetHashCode();
                if (this.ReplenishmentSource != null)
                    hashCode = hashCode * 59 + this.ReplenishmentSource.GetHashCode();
                if (this.ReplenishmentWarehouse != null)
                    hashCode = hashCode * 59 + this.ReplenishmentWarehouse.GetHashCode();
                if (this.Seasonality != null)
                    hashCode = hashCode * 59 + this.Seasonality.GetHashCode();
                if (this.ServiceLevel != null)
                    hashCode = hashCode * 59 + this.ServiceLevel.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.WarehouseID != null)
                    hashCode = hashCode * 59 + this.WarehouseID.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            foreach(var x in BaseValidate(validationContext)) yield return x;
            yield break;
        }
    }

}