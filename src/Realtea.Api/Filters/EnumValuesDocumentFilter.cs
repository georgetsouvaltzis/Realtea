using System;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Realtea.App.Enums;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Realtea.Api.Filters
{
    public class EnumValuesDocumentFilter : IDocumentFilter
    {
        private Dictionary<Type, List<string>> _applicableTypes = new Dictionary<Type, List<string>>
        {
            {typeof(DealTypeEnum), new List<string> { DealTypeEnum.Mortgage.ToString(), DealTypeEnum.Rental.ToString(), DealTypeEnum.Sale.ToString()} },
            {typeof(LocationEnum), new List<string> {LocationEnum.Batumi.ToString(), LocationEnum.Tbilisi.ToString(), LocationEnum.Tbilisi.ToString()} },
            {typeof(AdvertisementTypeEnum), new List<string> {AdvertisementTypeEnum.Free.ToString(), AdvertisementTypeEnum.Paid.ToString()} },
        };

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {

            foreach (var schema in swaggerDoc.Components.Schemas)
            {
                if (_applicableTypes.Any(x => x.Key.Name.Equals(schema.Key, StringComparison.OrdinalIgnoreCase)) && schema.Value.Enum.Count > 0)
                {
                    var applicableType = _applicableTypes.First(x => x.Key.Name.Equals(schema.Key, StringComparison.OrdinalIgnoreCase));

                    for(var i = 0; i < schema.Value.Enum.Count; i++)
                    {
                        var type = applicableType.Key.GetType();
                        var parsedValue = Enum.Parse(applicableType.Key, (schema.Value.Enum[i] as OpenApiInteger).Value.ToString());

                        schema.Value.Enum[i] = new OpenApiString(parsedValue.ToString());
                    }
                }
                if (schema.Value.Enum != null && schema.Value.Enum.Count > 0)
                {
                    var enumType = Type.GetType(schema.Value.Enum[0].ToString());
                    //if (enumType != null && enumType.IsEnum && customEnumValues.ContainsKey(enumType))
                    //{
                    //    // Replace each default enum value with its custom value
                    //    for (int i = 0; i < schema.Enum.Count; i++)
                    //    {
                    //        string enumValue = schema.Enum[i].ToString();
                    //        if (customEnumValues[enumType].ContainsKey(enumValue))
                    //        {
                    //            schema.Enum[i] = new OpenApiString(customEnumValues[enumType][enumValue]);
                    //        }
                    //    }
                    //}
                }
            }
        }
    }
}

