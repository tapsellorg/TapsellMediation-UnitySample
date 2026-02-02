using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tapsell.Mediation.Show.Native
{
    public class NativeAdView
    {
        public GameObject AdvertiserText { get; private set; }
        public GameObject CtaButton { get; private set; }
        public GameObject TitleText { get; private set; }
        public GameObject IconImage { get; private set; }
        public GameObject DescriptionText { get; private set; }
        public GameObject BannerImage { get; private set; }
        public GameObject AdChoicesImage { get; private set; }

        private NativeAdView()
        {
        }

        public class Builder
        {
            private readonly NativeAdView _adView;

            public Builder()
            {
                _adView = new NativeAdView();
            }

            public Builder WithAdvertiserText(GameObject text)
            {
                ValidateTextOrThrow(nameof(_adView.AdvertiserText), text);
                _adView.AdvertiserText = text;
                return this;
            }

            public Builder WithCtaButton(GameObject button)
            {
                ValidateButtonOrThrow(nameof(_adView.CtaButton), button);
                _adView.CtaButton = button;
                return this;
            }

            public Builder WithTitleText(GameObject text)
            {
                ValidateTextOrThrow(nameof(_adView.TitleText), text);
                _adView.TitleText = text;
                return this;
            }

            public Builder WithIconImage(GameObject image)
            {
                ValidateImageOrThrow(nameof(_adView.IconImage), image);
                _adView.IconImage = image;
                return this;
            }

            public Builder WithDescriptionText(GameObject text)
            {
                ValidateTextOrThrow(nameof(_adView.DescriptionText), text);
                _adView.DescriptionText = text;
                return this;
            }

            public Builder WithBannerImage(GameObject image)
            {
                ValidateImageOrThrow(nameof(_adView.BannerImage), image);
                _adView.BannerImage = image;
                return this;
            }

            public Builder WithAdChoicesImage(GameObject image)
            {
                _adView.AdChoicesImage = image;
                return this;
            }

            public NativeAdView Build()
            {
                return _adView;
            }

            private static void ValidateTextOrThrow(string fieldName, GameObject textObject)
            {
                if (textObject == null) return;
                CheckCollider(fieldName, textObject);

                if (textObject.GetComponent<Text>() == null && textObject.GetComponent<TMP_Text>() == null)
                    throw new InvalidOperationException(
                        $"NativeAdView.{fieldName} must have a Text (UnityEngine.UI.Text) or TMP_Text (TMPro.TMP_Text) component."
                    );
            }

            private static void ValidateButtonOrThrow(string fieldName, GameObject buttonObject)
            {
                if (buttonObject == null) return;
                CheckCollider(fieldName, buttonObject);

                if (buttonObject.GetComponent<Button>() == null)
                    throw new InvalidOperationException(
                        $"NativeAdView.{fieldName} must have a Button (UnityEngine.UI.Button) component."
                    );
            }

            private static void ValidateImageOrThrow(string fieldName, GameObject imageObject)
            {
                if (imageObject == null) return;
                CheckCollider(fieldName, imageObject);

                if (imageObject.GetComponent<Image>() == null && imageObject.GetComponent<RawImage>() == null)
                    throw new InvalidOperationException(
                        $"NativeAdView.{fieldName} must have an Image (UnityEngine.UI.Image) or RawImage (UnityEngine.UI.RawImage) component."
                    );
            }

            private static void CheckCollider(string fieldName, GameObject gameObject)
            {
                if (gameObject.GetComponent<Collider>() == null)
                    throw new InvalidOperationException($"NativeAdView.{fieldName} must have a Collider component.");
            }
        }
    }
}