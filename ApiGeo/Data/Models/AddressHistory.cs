using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGeo.Data.Models
{
    public class AddressHistory
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Country { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Province { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string ZipCode { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string City { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string StreetNumber { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Street { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string latitud { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string longitude { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string status { get; set; }
    }
}
