using System;
using System.ComponentModel.DataAnnotations;

namespace RepoDb.Core
{
    public class TableEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}