﻿namespace RealEstate.Api.Entities
{
    public class Images : BaseEntity
    {

        public int PropertyId { get; set; }
        public Properties Property { get; set; }
        public string Source { get; set; }
        public string Name { get; set; }

    }
}
