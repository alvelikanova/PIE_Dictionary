//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PIE_DB.Data_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Phonetic_matching
    {
        public decimal ID_Phonetic_matching { get; set; }
        public Nullable<decimal> ID_Phoneme_PIE { get; set; }
        public Nullable<decimal> ID_Phoneme_language { get; set; }
        public Nullable<decimal> ID_Language { get; set; }
        public Nullable<decimal> ID_Rule { get; set; }
        public string Comment { get; set; }
        public Nullable<decimal> Сonfidence_level { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual Phoneme Phoneme { get; set; }
        public virtual Phoneme Phoneme1 { get; set; }
        public virtual Rule Rule { get; set; }
    }
}
