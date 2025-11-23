using RepoDb.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demos.RepoDB.App.Domain
{
    [Table("[dbo].[User]")]
    public class User : TableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        //public Team Team { get; set; }
    }
}
