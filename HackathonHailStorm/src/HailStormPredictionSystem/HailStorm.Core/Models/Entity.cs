using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HailStorm.Core.Models
{
    public abstract class Entity
    {
        /// <summary>
        /// Entity identifier
        /// </summary>
        /// <example>4af3fc6a-cbac-9df0-8031-fdca0f682934</example>
        public string Id { get; set; }
    }
}
