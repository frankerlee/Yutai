namespace JLK.Catalog.VCT
{
    using System;

    public interface ICoField
    {
        string AliasName { get; set; }

        string DefaultValue { get; set; }

        bool Enable { get; set; }

        string ID { get; set; }

        int Length { get; set; }

        string Name { get; set; }

        int Precision { get; set; }

        bool Required { get; set; }

        CoFieldType Type { get; set; }
    }
}

