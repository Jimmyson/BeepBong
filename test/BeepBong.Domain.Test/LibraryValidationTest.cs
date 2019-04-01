using System;
using Xunit;
using BeepBong.Domain.Validation;
using BeepBong.Domain.Models;
using FluentValidation.TestHelper;

namespace BeepBong.Domain.Test
{
    public class LibraryValidationTest
    {
        private LibraryValidator validator = new LibraryValidator();

        // Album Name Checker
        [Fact]
        public void NullAlbumNameError()
        {
            Library l = new Library() {
                AlbumName = null
            };

            validator.ShouldHaveValidationErrorFor(library => library.AlbumName, l);
        }

        [Fact]
        public void UrlInAlbumNameError()
        {
            Library l = new Library() {
                AlbumName = "http"
            };

            validator.ShouldHaveValidationErrorFor(library => library.AlbumName, l);
        }

        // Label Checker
        [Fact]
        public void UrlInLabelError()
        {
            Library l = new Library() {
                Label = "http"
            };

            validator.ShouldHaveValidationErrorFor(library => library.Label, l);
        }

        // Catalog Checker
        [Fact]
        public void UrlInCatalogError()
        {
            Library l = new Library() {
                Catalog = "http"
            };

            validator.ShouldHaveValidationErrorFor(library => library.Catalog, l);
        }

        // MBID Checker
        [Fact]
        public void UrlInMBIDError()
        {
            Library l = new Library() {
                MBID = "http"
            };

            validator.ShouldHaveValidationErrorFor(library => library.MBID, l);
        }
    }
}