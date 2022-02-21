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
    /// Represents a single competitor in file. May be a part of team.
    /// </summary>
    [DataContract(Name = "CompetitorFileModel")]
    public partial class CompetitorFileModel : IEquatable<CompetitorFileModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitorFileModel" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CompetitorFileModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitorFileModel" /> class.
        /// </summary>
        /// <param name="name">Name of the competitor (required).</param>
        /// <param name="team">Team, if given..</param>
        public CompetitorFileModel(string name = default(string), string team = default(string))
        {
            // to ensure "name" is required (not null)
            if (name == null) {
                throw new ArgumentNullException("name is a required property for CompetitorFileModel and cannot be null");
            }
            this.Name = name;
            this.Team = team;
        }

        /// <summary>
        /// Name of the competitor
        /// </summary>
        /// <value>Name of the competitor</value>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Team, if given.
        /// </summary>
        /// <value>Team, if given.</value>
        [DataMember(Name = "team", EmitDefaultValue = true)]
        public string Team { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CompetitorFileModel {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Team: ").Append(Team).Append("\n");
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
            return this.Equals(input as CompetitorFileModel);
        }

        /// <summary>
        /// Returns true if CompetitorFileModel instances are equal
        /// </summary>
        /// <param name="input">Instance of CompetitorFileModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CompetitorFileModel input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Team == input.Team ||
                    (this.Team != null &&
                    this.Team.Equals(input.Team))
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
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                if (this.Team != null)
                {
                    hashCode = (hashCode * 59) + this.Team.GetHashCode();
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
