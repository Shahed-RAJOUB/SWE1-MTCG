using NUnit.Framework;
using Monster_Trading_Card_Game;
using NUnit.Framework.Internal;


namespace NUnitTest_MTCG
{
    public class BattleMethodsTests
    {
        [Test]
        public void TestMxMwithplayer1()
        {
            BattleMethod M = new BattleMethod();
            int result = M.MXM( 25 , 10 , 1 , 2);
            int Expected = 1 ;
            Assert.AreEqual(Expected, result);
        }
        [Test]
        public void TestMxMwithplayer2()
        {
            BattleMethod M = new BattleMethod();
            int result = M.MXM(25, 100, 1, 2);
            int Expected = 2;
            Assert.AreEqual(Expected, result);
        }
        [Test]
        public void TestMxMwithequalD()
        {
            BattleMethod M = new BattleMethod();
            int result = M.MXM(25, 25 , 1, 2);
            int Expected = -1;
            Assert.AreEqual(Expected, result);
        }

        public void TestSxSwithplayer2()
        {
            BattleMethod M = new BattleMethod();
            int result = M.SXS(50, "fire" ,50 ,"water", 1, 2);
            int Expected = 2;
            Assert.AreEqual(Expected, result);
        }
        [Test]
        public void TestSXSwithplayer1()
        {
            BattleMethod M = new BattleMethod();
            int result = M.SXS(50, "fire", 5, "water", 1, 2);
            int Expected = 1;
            Assert.AreEqual(Expected, result);
        }
        [Test]
        public void TestSxSwithequalD()
        {
            BattleMethod M = new BattleMethod();
            int result = M.SXS(50, "fire", 25, "water", 1, 2);
            int Expected = 2 ;
            Assert.AreEqual(Expected, result);
        }

        public void TestMxSwithplayer2()
        {
            BattleMethod M = new BattleMethod();
            int result = M.SXS(50, "fire", 50, "water", 1, 2);
            int Expected = 2;
            Assert.AreEqual(Expected, result);
        }
        [Test]
        public void TestMXSwithplayer1()
        {
            BattleMethod M = new BattleMethod();
            int result = M.SXS(50, "fire", 5, "water", 1, 2);
            int Expected = 1;
            Assert.AreEqual(Expected, result);
        }
        [Test]
        public void TestMxSwithequalD()
        {
            BattleMethod M = new BattleMethod();
            int result = M.SXS(50, "fire", 25, "water", 1, 2);
            int Expected = 2;
            Assert.AreEqual(Expected, result);
        }
    }
}