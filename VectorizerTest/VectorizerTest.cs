using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Generic.Collections.List;
using Generic.Maths;
using NUnit.Framework;
using Vectorizer.Logic.Picture;
using Vectorizer.Logic.Scoring;
using Vectorizer.Logic.Scoring.Incremental;
using Vectorizer.Logic.Scoring.PictureMutation;
using Vectorizer.Properties;
using Polygon = Vectorizer.Logic.Picture.Polygon;

namespace VectorizerTest
{
    [TestFixture]
    public class VectorizerTest
    {
        private const int TEST_POLYGON_COUNT = 10;
        private const int POLYGON_MAX_POINT_COUNT = 5;
        private readonly IDrawer[] drawers = new IDrawer[] {new BoxDrawer(), new PerYDrawer()};

        private readonly IntVector2 max = new IntVector2(20, 20);

        private readonly Func<PictureMutationAlgorithm, IScoringMethod>[] scorers
            = new Func<PictureMutationAlgorithm, IScoringMethod>[] {a => new FullEvaluationScoring(a), a => new ErrorDataScoring(a.SourceData)};

        private readonly IList<Bitmap> testImages = new List<Bitmap> {Resources.Polygon, Resources.Smiley, Resources.Square};
        private readonly IList<Polygon> testPolygons = new List<Polygon>(TEST_POLYGON_COUNT);

        public VectorizerTest()
        {
            var random = new Random();
            while (testPolygons.Count < TEST_POLYGON_COUNT)
            {
                var polygon = GetRandomPolygon(random, max, POLYGON_MAX_POINT_COUNT);
                if (!polygon.IsSelfIntersecting())
                    testPolygons.Add(polygon);
            }
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        public static Polygon GetRandomPolygon(Random random, IntVector2 max, int maxPointCount)
        {
            var result = new Polygon(Color.White);
            int pointCount = random.Next(3, maxPointCount);
            result.Points = new List<IntVector2>(maxPointCount);
            for (int pointId = 0; pointId < pointCount; pointId++)
            {
                int x = (int) random.RandomBetween(0, max.X);
                int y = (int) random.RandomBetween(0, max.Y);
                result.Points.Add(new IntVector2(x, y));
            }
            var colorComponents = new Byte[VColor.COMPONENT_COUNT];
            for (int i = 0; i < VColor.COMPONENT_COUNT; i++)
            {
                colorComponents[i] = (Byte) random.Next(0, 255);
            }
            result.Color = new VColor(colorComponents);
            return result;
        }

        [Test]
        public void ScoreDrawMethods()
        {
            foreach (var drawer in drawers)
            {
                var total = TimeSpan.Zero;
                foreach (var polygon in ListUtil.Repeat(() => testPolygons,10).SelectMany(x => x))
                {
                    var first = DateTime.Now;
                    drawer.Draw(polygon).ToList();
                    var then = DateTime.Now;
                    total += then - first;
                }
                Console.WriteLine("drawer " + drawer + " took " + total.TotalMilliseconds + " ms.");
            }
        }

        [Test]
        public void DrawMethodsAreEquivalent()
        {
            foreach(var polygon in testPolygons)
            {
                foreach(var drawerPair in drawers.HoldHandsLine())
                {
                    var points1 = drawerPair.Item1.Draw(polygon).ToList();
                    var points2 = drawerPair.Item2.Draw(polygon).ToList();
                    Assert.True(points1.SequenceEqual(points2));
                }
            }
        }

        [Test]
        public void NoPointDrawnTwice()
        {
            foreach (var polygon in testPolygons)
            {
                foreach (var drawer in drawers)
                {
                    var pointsDrawn = new HashSet<IntVector2>();
                    foreach(var x in drawer.Draw(polygon))
                    {
                        ((Action<IntVector2>)(point =>
                        {
                            if (pointsDrawn.Contains(point))
                                Assert.Fail(drawer.ToString());

                            pointsDrawn.Add(point);
                        }))(x);
                    }
                }
            }
        }

        [Test]
        public void ErrorDataFakeChangePolygonHasNoEffect()
        {
            foreach (var polygon in testPolygons)
            {
                var vectorPicture = new VectorPicture();
                vectorPicture.Polygons.Add(polygon);
                var mutation = new PolygonChanged(polygon, 0);
                var noMutation = new EmptyMutation();
                var testAlgorithms = testImages.Select(image => new TestAlgorithm(image, vectorPicture, PictureMutationAlgorithm.ScoringMethodEnum.FullEvaluationScoring));
                foreach (var scorer1 in testAlgorithms.Select(algorithm => new ErrorDataScoring(algorithm.SourceData)))
                {
                    scorer1.FoundBetter(vectorPicture);
                    Assert.AreEqual(scorer1.GetError(noMutation), scorer1.GetError(mutation));
                }
            }
        }

        [Test]
        public void ScoreMethodsAreEquivalent()
        {
            foreach (var polygonPair in testPolygons.ShakeHands())
            {
                var vectorPicture = new VectorPicture();
                vectorPicture.Polygons.Add(polygonPair.Item1);
                var mutation = new PolygonChanged(polygonPair.Item2, 0);
                CompareScoreMethods(vectorPicture, mutation);
            }
        }

        [Test]
        public void ScoreMethodsAreEquivalentWeak()
        {
            foreach (var polygon in testPolygons)
            {
                var vectorPicture = new VectorPicture();
                vectorPicture.Polygons.Add(polygon);
                var mutation = new PolygonChanged(polygon, 0);
                CompareScoreMethods(vectorPicture, mutation);
            }
        }

        [Test]
        public void ScoreMethodsAreEquivalentLeftRightPolygon()
        {
            var vectorPicture = new VectorPicture();
            vectorPicture.Polygons.Add(LeftPolygon());
            var mutation = new PolygonChanged(RightPolygon(), 0);
            CompareScoreMethods(vectorPicture,mutation,Properties.Resources.whiteRectangle);
        }

        [Test]
        public void FindIntersection1()
        {
            var polygon = new Polygon(Color.White);
            polygon.AddPoint(33,18);
            polygon.AddPoint(25, 1);
            polygon.AddPoint(8, 13);
            polygon.AddPoint(13, 26);
            polygon.AddPoint(34, 36);
            polygon.AddPoint(28, 14);
            polygon.AddPoint(30, 19);
            polygon.AddPoint(30, 9);
            Assert.IsTrue(polygon.IsSelfIntersecting());
        }

        [Test]
        public void FindIntersection2()
        {
            var polygon = new Polygon(Color.White);
            polygon.AddPoint(23, 4);
            polygon.AddPoint(12, 36);
            polygon.AddPoint(36, 34); //edge1
            polygon.AddPoint(23, 13); //edge1
            polygon.AddPoint(28,24); //edge2
            polygon.AddPoint(28,18); //edge2
            Assert.IsTrue(polygon.IsSelfIntersecting());
        }

        [Test]
        public void ScoreMethodsAreEquivalentLeftMidPolygon()
        {
            var vectorPicture = new VectorPicture();
            vectorPicture.Polygons.Add(LeftPolygon());
            var mutation = new PolygonChanged(MidPolygon(), 0);
            CompareScoreMethods(vectorPicture, mutation, Properties.Resources.whiteRectangle);
        }

        private void CompareScoreMethods(VectorPicture vectorPicture, PolygonChanged mutation)
        {
            foreach (var image in testImages)
            {
                CompareScoreMethods(vectorPicture,mutation,image);
            }
        }

        [Test]
        public void ScoreMethodsAreEquivalentCase1()
        {
            var polygon = new Polygon(Color.White);
            polygon.AddPoint(8,13);
            polygon.AddPoint(7,1);
            polygon.AddPoint(11,14);
            var picture = new VectorPicture();
            picture.Polygons.Add(polygon);
            var mutationPolygon = new Polygon(Color.White);
            mutationPolygon.AddPoint(1, 4);
            mutationPolygon.AddPoint(13, 0);
            mutationPolygon.AddPoint(17, 15);
            mutationPolygon.AddPoint(2, 10);
            var mutation = new PolygonChanged(mutationPolygon, 0);
            CompareScoreMethods(picture,mutation,Resources.Polygon);
        }

        [Test]
        public void ScoreMethodsAreEquivalentCase2()
        {
            var polygon = new Polygon(Color.White);
            polygon.AddPoint(5, 7);
            polygon.AddPoint(10, 5);
            polygon.AddPoint(7, 15);
            var picture = new VectorPicture();
            picture.Polygons.Add(polygon);
            var mutationPolygon = new Polygon(Color.White);
            mutationPolygon.AddPoint(6, 0);
            mutationPolygon.AddPoint(5, 0);
            mutationPolygon.AddPoint(19, 18);
            mutationPolygon.AddPoint(18, 1);
            var mutation = new PolygonChanged(mutationPolygon, 0);
            CompareScoreMethods(picture, mutation, Resources.Polygon);
        }

        private void CompareScoreMethods(VectorPicture vectorPicture, PolygonChanged mutation, Bitmap image)
        {
            var algorithm = new TestAlgorithm(image, vectorPicture, PictureMutationAlgorithm.ScoringMethodEnum.FullEvaluationScoring);
            var scores = new List<double>(scorers.Length);
            foreach (var scorer in scorers.Select(sf => sf(algorithm)))
            {
                algorithm.SetInitial();
                scorer.FoundBetter(vectorPicture);
                scores.Add(scorer.GetError(mutation));
            }
            Assert.IsTrue(scores.Distinct().Count() == 1);
        }

        private static Polygon LeftPolygon()
        {
            var result = new Polygon(Color.White);
            result.Points.Add(new IntVector2(0, 0));
            result.Points.Add(new IntVector2(5, 0));
            result.Points.Add(new IntVector2(5, 10));
            result.Points.Add(new IntVector2(0, 10));
            return result;
        }

        private Polygon MidPolygon()
        {
            var result = new Polygon(Color.White);
            result.Points.Add(new IntVector2(3, 0));
            result.Points.Add(new IntVector2(8, 0));
            result.Points.Add(new IntVector2(8, 10));
            result.Points.Add(new IntVector2(3, 10));
            return result;
        }

        private Polygon RightPolygon()
        {
            var result = new Polygon(Color.White);
            result.Points.Add(new IntVector2(5, 0));
            result.Points.Add(new IntVector2(10, 0));
            result.Points.Add(new IntVector2(10, 10));
            result.Points.Add(new IntVector2(5, 10));
            return result;
        }
    }
}