using System;
using Xunit;
using BeepBong.Domain.Validation;
using BeepBong.Domain.Models;
using FluentValidation.TestHelper;

namespace BeepBong.Domain.Test
{
    public class ChannelValidationTest
    {
        private ChannelValidator validator = new ChannelValidator();

        //  Name Checker
        [Fact]
        public void NullNameError()
        {
            Channel c = new Channel() {
                Name = null
            };

            validator.ShouldHaveValidationErrorFor(Channel => Channel.Name, c);
        }

        [Fact]
        public void UrlInNameError()
        {
            Channel c = new Channel() {
                Name = "http"
            };

            validator.ShouldHaveValidationErrorFor(Channel => Channel.Name, c);
        }

        // Organisation Checker
        [Fact]
        public void UrlInOrganisationError()
        {
            Channel c = new Channel() {
                Organisation = "http"
            };

            validator.ShouldHaveValidationErrorFor(Channel => Channel.Organisation, c);
        }
    }
}