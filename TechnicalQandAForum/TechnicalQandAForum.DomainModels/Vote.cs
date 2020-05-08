using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalQandAForum.DomainModels
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteId { get; set; }
        public int UserId { get; set; }
        public int AnswerId { get; set; }
        public int VoteValue { get; set; }
    }
}