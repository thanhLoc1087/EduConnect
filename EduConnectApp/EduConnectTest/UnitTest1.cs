using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EduConnectTest
{
    [TestClass]
    public class UnitTest1
    {
            [TestMethod]
            public void AverageScoreCheck()
            {
                AverageScoreCheck averageScore = new AverageScoreCheck();
                string[] scores2 = { "8", "", "9", "10", "", "8", "9" };

                Assert.Multiple(() =>
                {
                    Assert.That(averageScore.CalculateAverageScore(scores2), Is.EqualTo(8.75));

                    string[] scores3 = { "", "8", "9", "10", "5", "8", "9" };
                    Assert.That(averageScore.CalculateAverageScore(scores3), Is.EqualTo(8));

                    string[] scores4 = { "0", "0", "0", "0", "0", "0", "0" };
                    Assert.That(averageScore.CalculateAverageScore(scores4), Is.EqualTo(0));

                    string[] scores1 = { "8", "7", "9", "10", "6", "8", "9" };
                    Assert.That(averageScore.CalculateAverageScore(scores1), Is.EqualTo(8.27));
                });
            }
            [TestMethod]
            public void TestDiem()
            {
                AverageScoreCheck averageScore = new AverageScoreCheck();

                string gioi = "giỏi";
                string kha = "Khá";
                string trungbinh = "Trung bình";
                string yeu = "Yếu";
                Assert.Multiple(() =>
                {
                    Assert.That(averageScore.checkStudentLevel(11, "tốt"), Is.EqualTo(null));
                    Assert.That(averageScore.checkStudentLevel(8.5, "tốt"), Is.EqualTo(gioi));
                    Assert.That(averageScore.checkStudentLevel(7, "tốt"), Is.EqualTo(kha));
                    Assert.That(averageScore.checkStudentLevel(4, "tốt"), Is.EqualTo(yeu));
                    Assert.That(averageScore.checkStudentLevel(-1, "tốt"), Is.EqualTo(null));


                    Assert.That(averageScore.checkStudentLevel(11, "Khá"), Is.EqualTo(null));
                    Assert.That(averageScore.checkStudentLevel(8.5, "Khá"), Is.EqualTo(kha));
                    Assert.That(averageScore.checkStudentLevel(7, "Khá"), Is.EqualTo(kha));
                    Assert.That(averageScore.checkStudentLevel(4, "Khá"), Is.EqualTo(yeu));
                    Assert.That(averageScore.checkStudentLevel(-1, "Khá"), Is.EqualTo(null));

                    Assert.That(averageScore.checkStudentLevel(11, "Trung bình"), Is.EqualTo(null));
                    Assert.That(averageScore.checkStudentLevel(8.5, "Trung bình"), Is.EqualTo(trungbinh));
                    Assert.That(averageScore.checkStudentLevel(7, "Trung bình"), Is.EqualTo(trungbinh));
                    Assert.That(averageScore.checkStudentLevel(4, "Trung bình"), Is.EqualTo(yeu));
                    Assert.That(averageScore.checkStudentLevel(-1, "Trung bình"), Is.EqualTo(null));

                    Assert.That(averageScore.checkStudentLevel(11, "Yếu"), Is.EqualTo(null));
                    Assert.That(averageScore.checkStudentLevel(8.5, "Yếu"), Is.EqualTo(yeu));
                    Assert.That(averageScore.checkStudentLevel(7, "Yếu"), Is.EqualTo(yeu));
                    Assert.That(averageScore.checkStudentLevel(4, "Yếu"), Is.EqualTo(yeu));
                    Assert.That(averageScore.checkStudentLevel(-1, "Yếu"), Is.EqualTo(null));
                });
            }
        }
    
}
