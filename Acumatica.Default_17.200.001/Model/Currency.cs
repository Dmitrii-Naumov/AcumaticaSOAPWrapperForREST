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
    /// Currency
    /// </summary>
    [DataContract]
    public partial class Currency : Entity,  IEquatable<Currency>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Currency" /> class.
        /// </summary>
        /// <param name="active">active.</param>
        /// <param name="createdDateTime">createdDateTime.</param>
        /// <param name="currencyID">currencyID.</param>
        /// <param name="currencySymbol">currencySymbol.</param>
        /// <param name="decimalPrecision">decimalPrecision.</param>
        /// <param name="description">description.</param>
        /// <param name="lastModifiedDateTime">lastModifiedDateTime.</param>
        /// <param name="useForAccounting">useForAccounting.</param>
        public Currency(BooleanValue active = default(BooleanValue), DateTimeValue createdDateTime = default(DateTimeValue), StringValue currencyID = default(StringValue), StringValue currencySymbol = default(StringValue), ShortValue decimalPrecision = default(ShortValue), StringValue description = default(StringValue), DateTimeValue lastModifiedDateTime = default(DateTimeValue), BooleanValue useForAccounting = default(BooleanValue), Guid? id = default(Guid?), long? rowNumber = default(long?), string note = default(string), Dictionary<string, Dictionary<string, CustomField>> custom = default(Dictionary<string, Dictionary<string, CustomField>>), List<FileLink> files = default(List<FileLink>)) : base(id, rowNumber, note, custom, files)
        {
            this.Active = active;
            this.CreatedDateTime = createdDateTime;
            this.CurrencyID = currencyID;
            this.CurrencySymbol = currencySymbol;
            this.DecimalPrecision = decimalPrecision;
            this.Description = description;
            this.LastModifiedDateTime = lastModifiedDateTime;
            this.UseForAccounting = useForAccounting;
        }
        
        /// <summary>
        /// Gets or Sets Active
        /// </summary>
        [DataMember(Name="Active", EmitDefaultValue=false)]
        public BooleanValue Active { get; set; }

        /// <summary>
        /// Gets or Sets CreatedDateTime
        /// </summary>
        [DataMember(Name="CreatedDateTime", EmitDefaultValue=false)]
        public DateTimeValue CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or Sets CurrencyID
        /// </summary>
        [DataMember(Name="CurrencyID", EmitDefaultValue=false)]
        public StringValue CurrencyID { get; set; }

        /// <summary>
        /// Gets or Sets CurrencySymbol
        /// </summary>
        [DataMember(Name="CurrencySymbol", EmitDefaultValue=false)]
        public StringValue CurrencySymbol { get; set; }

        /// <summary>
        /// Gets or Sets DecimalPrecision
        /// </summary>
        [DataMember(Name="DecimalPrecision", EmitDefaultValue=false)]
        public ShortValue DecimalPrecision { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name="Description", EmitDefaultValue=false)]
        public StringValue Description { get; set; }

        /// <summary>
        /// Gets or Sets LastModifiedDateTime
        /// </summary>
        [DataMember(Name="LastModifiedDateTime", EmitDefaultValue=false)]
        public DateTimeValue LastModifiedDateTime { get; set; }

        /// <summary>
        /// Gets or Sets UseForAccounting
        /// </summary>
        [DataMember(Name="UseForAccounting", EmitDefaultValue=false)]
        public BooleanValue UseForAccounting { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Currency {\n");
            sb.Append("  ").Append(base.ToString().Replace("\n", "\n  ")).Append("\n");
            sb.Append("  Active: ").Append(Active).Append("\n");
            sb.Append("  CreatedDateTime: ").Append(CreatedDateTime).Append("\n");
            sb.Append("  CurrencyID: ").Append(CurrencyID).Append("\n");
            sb.Append("  CurrencySymbol: ").Append(CurrencySymbol).Append("\n");
            sb.Append("  DecimalPrecision: ").Append(DecimalPrecision).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  LastModifiedDateTime: ").Append(LastModifiedDateTime).Append("\n");
            sb.Append("  UseForAccounting: ").Append(UseForAccounting).Append("\n");
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
            return this.Equals(input as Currency);
        }

        /// <summary>
        /// Returns true if Currency instances are equal
        /// </summary>
        /// <param name="input">Instance of Currency to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Currency input)
        {
            if (input == null)
                return false;

            return base.Equals(input) && 
                (
                    this.Active == input.Active ||
                    (this.Active != null &&
                    this.Active.Equals(input.Active))
                ) && base.Equals(input) && 
                (
                    this.CreatedDateTime == input.CreatedDateTime ||
                    (this.CreatedDateTime != null &&
                    this.CreatedDateTime.Equals(input.CreatedDateTime))
                ) && base.Equals(input) && 
                (
                    this.CurrencyID == input.CurrencyID ||
                    (this.CurrencyID != null &&
                    this.CurrencyID.Equals(input.CurrencyID))
                ) && base.Equals(input) && 
                (
                    this.CurrencySymbol == input.CurrencySymbol ||
                    (this.CurrencySymbol != null &&
                    this.CurrencySymbol.Equals(input.CurrencySymbol))
                ) && base.Equals(input) && 
                (
                    this.DecimalPrecision == input.DecimalPrecision ||
                    (this.DecimalPrecision != null &&
                    this.DecimalPrecision.Equals(input.DecimalPrecision))
                ) && base.Equals(input) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && base.Equals(input) && 
                (
                    this.LastModifiedDateTime == input.LastModifiedDateTime ||
                    (this.LastModifiedDateTime != null &&
                    this.LastModifiedDateTime.Equals(input.LastModifiedDateTime))
                ) && base.Equals(input) && 
                (
                    this.UseForAccounting == input.UseForAccounting ||
                    (this.UseForAccounting != null &&
                    this.UseForAccounting.Equals(input.UseForAccounting))
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
                if (this.Active != null)
                    hashCode = hashCode * 59 + this.Active.GetHashCode();
                if (this.CreatedDateTime != null)
                    hashCode = hashCode * 59 + this.CreatedDateTime.GetHashCode();
                if (this.CurrencyID != null)
                    hashCode = hashCode * 59 + this.CurrencyID.GetHashCode();
                if (this.CurrencySymbol != null)
                    hashCode = hashCode * 59 + this.CurrencySymbol.GetHashCode();
                if (this.DecimalPrecision != null)
                    hashCode = hashCode * 59 + this.DecimalPrecision.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.LastModifiedDateTime != null)
                    hashCode = hashCode * 59 + this.LastModifiedDateTime.GetHashCode();
                if (this.UseForAccounting != null)
                    hashCode = hashCode * 59 + this.UseForAccounting.GetHashCode();
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