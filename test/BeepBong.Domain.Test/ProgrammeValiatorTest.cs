using System;
using Xunit;
using BeepBong.Domain.Validation;
using BeepBong.Domain.Models;
using FluentValidation.TestHelper;

namespace BeepBong.Domain.Test
{
    public class ProgrammeValidatorTest
    {
        private ProgrammeValidator validator = new ProgrammeValidator();

        // Name Error Checking
        [Fact]
        public void NullNameError()
        {
            Programme p = new Programme() {
                Name = null
            };

            validator.ShouldHaveValidationErrorFor(programme => programme.Name, p);
        }

        [Fact]
        public void UrlInNameError()
        {
            Programme p = new Programme() {
                Name = "http"
            };

            validator.ShouldHaveValidationErrorFor(programme => programme.Name, p);
        }

        // Year Error Checking
        [Fact]
        public void NullYearError()
        {
            Programme p = new Programme() {
                Year = null
            };

            validator.ShouldHaveValidationErrorFor(programme => programme.Year, p);
        }

        [Fact]
        public void FiveDigitYearError()
        {
            Programme p = new Programme() {
                Year = "12732"
            };

            validator.ShouldHaveValidationErrorFor(programme => programme.Year, p);
        }

        [Fact]
        public void ThreeDigitYearError()
        {
            Programme p = new Programme() {
                Year = "127"
            };

            validator.ShouldHaveValidationErrorFor(programme => programme.Year, p);
        }

        [Fact]
        public void FourDigitYearError()
        {
            Programme p = new Programme() {
                Year = "1273"
            };

            validator.ShouldNotHaveValidationErrorFor(programme => programme.Year, p);
        }

        [Fact]
        public void UrlInYearError()
        {
            Programme p = new Programme() {
                Name = "http"
            };

            validator.ShouldHaveValidationErrorFor(programme => programme.Year, p);
        }
		
        [Fact]
        public void MixedCharYearError()
        {
            Programme p = new Programme() {
                Name = "y33t"
            };

            validator.ShouldHaveValidationErrorFor(programme => programme.Year, p);
        }
    }
}
