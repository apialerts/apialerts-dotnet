using APIAlerts.util;
using Xunit;

namespace APIAlerts.Tests.util
{
    public class LoggerTests
    {
        [Fact]
        public void Success_DebugEnabled_PrintsMessage()
        {
            var logger = new Logger();
            logger.Configure(debug: true);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            logger.Success("Test success message");

            var expected = "✓ (apialerts.com) Test success message" + Environment.NewLine;
            Assert.Equal(expected, sw.ToString());
        }

        [Fact]
        public void Warning_DebugEnabled_PrintsMessage()
        {
            var logger = new Logger();
            logger.Configure(debug: true);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            logger.Warning("Test warning message");

            var expected = "! (apialerts.com) Warning: Test warning message" + Environment.NewLine;
            Assert.Equal(expected, sw.ToString());
        }

        [Fact]
        public void Error_DebugEnabled_PrintsMessage()
        {
            var logger = new Logger();
            logger.Configure(debug: true);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            logger.Error("Test error message");

            var expected = "x (apialerts.com) Error: Test error message" + Environment.NewLine;
            Assert.Equal(expected, sw.ToString());
        }

        [Fact]
        public void Success_DebugDisabled_DoesNotPrintMessage()
        {
            var logger = new Logger();
            logger.Configure(debug: false);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            logger.Success("Test success message");

            Assert.Empty(sw.ToString());
        }

        [Fact]
        public void Warning_DebugDisabled_DoesNotPrintMessage()
        {
            var logger = new Logger();
            logger.Configure(debug: false);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            logger.Warning("Test warning message");

            Assert.Empty(sw.ToString());
        }

        [Fact]
        public void Error_DebugDisabled_DoesNotPrintMessage()
        {
            var logger = new Logger();
            logger.Configure(debug: false);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            logger.Error("Test error message");

            Assert.Empty(sw.ToString());
        }
    }
}