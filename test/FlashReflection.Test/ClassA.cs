using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashReflection.Test
{
    [Serializable]
    [DisplayColumn(nameof(ClassA.PropertyString1))]
    public class ClassA
    {
        public string PropertyString1 { get; set; }
        [Required]
        public string PropertyString2 { get; set; }
        public int PropertyInt1 { get; set; }

        [Required]
        public int PropertyInt2 { get; set; }
        public Guid PropertyGuid1 { get; set; }

        public Guid? PropertyGuid2 { get; set; }

        public DateTime? PropertyDateTime1 { get; set; }

        public void MethodVoid()
        { }

        public int MethodReturnInt()
        {
            return 0;
        }
    }
}
