//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.1.4.0 (Newtonsoft.Json v12.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------
using Elements;
using Elements.GeoJSON;
using Elements.Geometry;
using Elements.Geometry.Solids;
using Elements.Properties;
using Elements.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using Line = Elements.Geometry.Line;
using Polygon = Elements.Geometry.Polygon;

namespace Elements
{
    #pragma warning disable // Disable all warnings

    /// <summary>An element with a geometric representation.</summary>
    [Newtonsoft.Json.JsonConverter(typeof(Elements.Serialization.JSON.JsonInheritanceConverter), "discriminator")]
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.4.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class GeometricElement : Element
    {
        [Newtonsoft.Json.JsonConstructor]
        public GeometricElement(Transform @transform, Material @material, Representation @representation, bool @isElementDefinition, System.Guid @id, string @name)
            : base(id, name)
        {
            var validator = Validator.Instance.GetFirstValidatorForType<GeometricElement>();
            if(validator != null)
            {
                validator.PreConstruct(new object[]{ @transform, @material, @representation, @isElementDefinition, @id, @name});
            }
        
            this.Transform = @transform;
            this.Material = @material;
            this.Representation = @representation;
            this.IsElementDefinition = @isElementDefinition;
        
            if(validator != null)
            {
                validator.PostConstruct(this);
            }
        }
    
        /// <summary>The element's transform.</summary>
        [Newtonsoft.Json.JsonProperty("Transform", Required = Newtonsoft.Json.Required.AllowNull)]
        public Transform Transform { get; set; }
    
        /// <summary>The element's material.</summary>
        [Newtonsoft.Json.JsonProperty("Material", Required = Newtonsoft.Json.Required.AllowNull)]
        public Material Material { get; set; }
    
        /// <summary>The element's representation.</summary>
        [Newtonsoft.Json.JsonProperty("Representation", Required = Newtonsoft.Json.Required.AllowNull)]
        public Representation Representation { get; set; }
    
        /// <summary>When true, this element will act as the base definition for element instances, and will not appear in visual output.</summary>
        [Newtonsoft.Json.JsonProperty("IsElementDefinition", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool IsElementDefinition { get; set; } = false;
    
    
    }
}