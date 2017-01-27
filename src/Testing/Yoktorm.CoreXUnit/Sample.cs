using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Yoktorm.CoreXUnit
{
    public static class Sample
    {
        [Fact]
        public static void _Do()
        {
            Assert.Equal(1, 1);
        }
    }
}
