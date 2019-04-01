using System;
using Xunit;
using BeepBong.Domain.Validation;
using BeepBong.Domain.Models;
using FluentValidation.TestHelper;

namespace BeepBong.Domain.Test
{
    public class SampleValidatorTest
    {
        private SampleValidator validator = new SampleValidator();

        // Sample Rate Error Checking
        [Fact]
        public void EmptySampleRateError()
        {
            Sample s = new Sample() {
                SampleRate = 0
            };

            validator.ShouldHaveValidationErrorFor(sample => sample.SampleRate, s);
        }
        
        [Fact]
        public void NegativeSampleRateError()
        {
            Sample s = new Sample() {
                SampleRate = -20
            };

            validator.ShouldHaveValidationErrorFor(sample => sample.SampleRate, s);
        }

        // Sample Count Error Checking
        [Fact]
        public void EmptySampleCountError()
        {
            Sample s = new Sample() {
                SampleCount = 0
            };

            validator.ShouldHaveValidationErrorFor(sample => sample.SampleCount, s);
        }
        
        [Fact]
        public void NegativeSampleCountError()
        {
            Sample s = new Sample() {
                SampleCount = -20
            };

            validator.ShouldHaveValidationErrorFor(sample => sample.SampleCount, s);
        }

        // Channel Error Checking
        [Fact]
        public void EmptyChannelCountError()
        {
            Sample s = new Sample() {
                Channels = 0
            };

            validator.ShouldHaveValidationErrorFor(sample => sample.Channels, s);
        }
        
        [Fact]
        public void NegativeChannelError()
        {
            Sample s = new Sample() {
                Channels = -20
            };

            validator.ShouldHaveValidationErrorFor(sample => sample.Channels, s);
        }

        // Bit Rate Checking
        [Fact]
        public void EmptyBitRateError()
        {
            Sample s = new Sample() {
                BitRate = 0
            };

            validator.ShouldHaveValidationErrorFor(sample => sample.BitRate, s);
        }
        
        [Fact]
        public void NegativeBitRateError()
        {
            Sample s = new Sample() {
                BitRate = -20
            };

            validator.ShouldHaveValidationErrorFor(sample => sample.BitRate, s);
        }

        // Codec Checking
        [Fact]
        public void EmptyCodecError()
        {
            Sample s = new Sample() {
                Codec = null
            };

            validator.ShouldHaveValidationErrorFor(sample => sample.Codec, s);
        }
        
        [Fact]
        public void UrlInCodecError()
        {
            Sample s = new Sample() {
                Codec = "http"
            };

            validator.ShouldHaveValidationErrorFor(sample => sample.Codec, s);
        }

        // Notes Checking
        [Fact]
        public void EmptyNotesPass()
        {
            Sample s = new Sample() {
                Notes = null
            };

            validator.ShouldNotHaveValidationErrorFor(sample => sample.Notes, s);
        }
        
        [Fact]
        public void UrlInNotesError()
        {
            Sample s = new Sample() {
                Notes = "http"
            };

            validator.ShouldHaveValidationErrorFor(sample => sample.Notes, s);
        }
    }
}