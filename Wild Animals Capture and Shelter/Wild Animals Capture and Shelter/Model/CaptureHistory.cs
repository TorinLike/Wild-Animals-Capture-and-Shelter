//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wild_Animals_Capture_and_Shelter.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CaptureHistory
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public int SpeciesID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    
        public virtual Species Species { get; set; }
    }
}
