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
    
    public partial class Phoneme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phoneme()
        {
            this.Phonetic_matching = new HashSet<Phonetic_matching>();
            this.Phonetic_matching1 = new HashSet<Phonetic_matching>();
        }
    
        public decimal ID_Phoneme { get; set; }
        public string Record { get; set; }
        public string Transcrip { get; set; }
        public string Phoneme_desc { get; set; }
        public Nullable<bool> Vowel_consonant { get; set; }
        public Nullable<bool> Reconsr { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phonetic_matching> Phonetic_matching { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phonetic_matching> Phonetic_matching1 { get; set; }
    }
}