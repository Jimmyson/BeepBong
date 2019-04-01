using System;
using Xunit;
using BeepBong.Domain.Validation;
using BeepBong.Domain.Models;
using FluentValidation.TestHelper;
using System.Collections.Generic;

namespace BeepBong.Domain.Test
{
    public class TrackValidatorTest
    {
        private TrackValidator validator = new TrackValidator();

        // Name Error Checking
        [Fact]
        public void NullNameError()
        {
            Track t = new Track() {
                Name = null
            };

            validator.ShouldHaveValidationErrorFor(track => track.Name, t);
        }

        [Fact]
        public void UrlInNameError()
        {
            Track t = new Track() {
                Name = "http"
            };

            validator.ShouldHaveValidationErrorFor(track => track.Name, t);
        }

        // Subtitle Error Checking
        [Fact]
        public void UrlInSubtitleError()
        {
            Track t = new Track() {
                Subtitle = "http"
            };

            validator.ShouldHaveValidationErrorFor(track => track.Subtitle, t);
        }

        // Track Error Checking
        [Fact]
        public void SamplesForLibraryFail()
        {
            List<Sample> samples = new List<Sample>()
            {
                new Sample() {
                    Notes = "Sample1"
                },
                new Sample() {
                    Notes = "Sample2"
                }
            };
            Programme p = new Programme()
            {
                IsLibraryMusic = true
            };
            Track t = new Track()
            {
                Programme = p,
                Samples = samples
            };

            validator.ShouldHaveValidationErrorFor(track => track.Samples, t);
        }
    }
}