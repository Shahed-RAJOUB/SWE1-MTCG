using NUnit.Framework;
using Monster_Trading_Card_Game;

namespace NUnitTests_MTCG
{
    public class Server_test
    {
     

        [Test]
        public void Server_Test_GetRequest ()
        {
            var Program = new Server();
            Program.Connection();

            // Assert.Pass();
            Assert.AreEqual();
        }

    }
}