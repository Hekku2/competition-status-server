/*
 * Competition Status API
 *
 * API which provides status information for sports competition.                          See https://github.com/Hekku2/competition-status-server/ for more information.
 *
 * The version of the OpenAPI document: v1
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Org.OpenAPITools.Client.OpenAPIDateConverter;

namespace Org.OpenAPITools.Model
{
    /// <summary>
    /// ScoreboardStatusModel
    /// </summary>
    [DataContract(Name = "ScoreboardStatusModel")]
    public partial class ScoreboardStatusModel : IEquatable<ScoreboardStatusModel>, IValidatableObject
    {

        /// <summary>
        /// Gets or Sets ScoreboardMode
        /// </summary>
        [DataMember(Name = "scoreboardMode", IsRequired = true, EmitDefaultValue = false)]
        public ScoreboardModeModel ScoreboardMode { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreboardStatusModel" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected ScoreboardStatusModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreboardStatusModel" /> class.
        /// </summary>
        /// <param name="scoreboardMode">scoreboardMode (required).</param>
        /// <param name="result">result.</param>
        public ScoreboardStatusModel(ScoreboardModeModel scoreboardMode = default(ScoreboardModeModel), PerformanceResultsContentModel result = default(PerformanceResultsContentModel))
        {
            this.ScoreboardMode = scoreboardMode;
            this.Result = result;
        }

        /// <summary>
        /// Gets or Sets Result
        /// </summary>
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public PerformanceResultsContentModel Result { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ScoreboardStatusModel {\n");
            sb.Append("  ScoreboardMode: ").Append(ScoreboardMode).Append("\n");
            sb.Append("  Result: ").Append(Result).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as ScoreboardStatusModel);
        }

        /// <summary>
        /// Returns true if ScoreboardStatusModel instances are equal
        /// </summary>
        /// <param name="input">Instance of ScoreboardStatusModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ScoreboardStatusModel input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.ScoreboardMode == input.ScoreboardMode ||
                    this.ScoreboardMode.Equals(input.ScoreboardMode)
                ) && 
                (
                    this.Result == input.Result ||
                    (this.Result != null &&
                    this.Result.Equals(input.Result))
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
                int hashCode = 41;
                hashCode = (hashCode * 59) + this.ScoreboardMode.GetHashCode();
                if (this.Result != null)
                {
                    hashCode = (hashCode * 59) + this.Result.GetHashCode();
                }
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
