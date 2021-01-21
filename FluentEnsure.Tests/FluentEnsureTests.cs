using System;
using FluentAssertions;
using Xunit;

namespace FluentEnsure.Tests
{
    public class FluentEnsureTests
    {
        private readonly IBeforeRule<object> _ensure;

        public FluentEnsureTests()
        {
            _ensure = FluentEnsure.StartEnsure();
        }

        [Fact]
        public void GetResult_Succeeds()
        {
            var result = _ensure.That(true).GetResult();

            result.Should().BeTrue();
        }

        [Fact]
        public void ThatNotLazy_Succeeds()
        {
            Action act = () =>
            {
                _ensure.That(true).OrThrow();
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void That_Generic_Succeeds()
        {
            Action act = () =>
            {
                _ensure.That(myObject => true).OrThrow();
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void That_Succeeds()
        {
            Action act = () =>
            {
                _ensure.That(() => true).OrThrow();
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void That_Throws_DefaultException()
        {
            Action act = () =>
            {
                _ensure.That(() => false).OrThrow();
            };

            act.Should().ThrowExactly<Exception>();
        }

        [Fact]
        public void That_Throws_SpecificException()
        {
            var message = "customMessage";

            Action act = () =>
            {
                _ensure.That(() => false, new ApplicationException(message)).OrThrow();
            };

            act.Should().ThrowExactly<ApplicationException>().And.Message.Should().Be(message);
        }

        [Fact]
        public void That_WithAnd_Succeeds()
        {
            Action act = () =>
            {
                _ensure.That(() => true).And().That(() => true).OrThrow();
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void That_WithAnd_Throws_DefaultException()
        {
            Action act = () =>
            {
                _ensure.That(() => true).And().That(() => false).OrThrow();
            };

            act.Should().ThrowExactly<Exception>();
        }

        [Fact]
        public void That_WithOr_Succeeds()
        {
            Action act = () =>
            {
                _ensure.That(() => false).Or().That(() => true).OrThrow();
            };

            act.Should().NotThrow();
        }

        [Fact]
        public void That_WithOr_Throws_DefaultException()
        {
            Action act = () =>
            {
                _ensure.That(() => false).Or().That(() => false).OrThrow();
            };

            act.Should().ThrowExactly<Exception>();
        }

        [Fact]
        public void That_WithContext_Succeeds()
        {
            var myComplexObject = new { prop1 = "myProp", prop2 = new { subProp = new DateTime(2021, 01, 01) } };
            Action act = () =>
            {
                _ensure.WithContext(myComplexObject).That(s => s.prop2.subProp.Year == 2021).OrThrow();
            };

            act.Should().NotThrow();
        }


    }
}
