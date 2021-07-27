﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSOSProject.DomainModels
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VodeID { get; set; }

        public int UserID { get; set; }

        public int AnswerID { get; set; }

        public int VoteValue { get; set; }
    }
}