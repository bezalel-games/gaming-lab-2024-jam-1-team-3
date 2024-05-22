using System;
using UnityEngine;

namespace BrewedInk.CRT
{
	
	// [ImageEffectAllowedInSceneView] // ATTENTION: Uncomment this to see the effect in scene view.
	[RequireComponent(typeof(Camera))]
	[ExecuteAlways]
	public class CRTCameraBehaviour : MonoBehaviour
	{
		[Header("Configuration")] 
		public CRTDataObject startConfig;
		public CRTRenderSettingsObject crtRenderSettings;
		
		[Header("Runtime Data (edit with care!)")]
		public Material _runtimeMaterial;
		public CRTData data;

		private string lastValidationId;

		

		[ContextMenu("Reset Material")]
		public void ResetMaterial()
		{
			DestroyMaterial();
			CreateMaterial();
		}
	
		private void OnDestroy()
		{
			DestroyMaterial();
		}

		void DestroyMaterial()
		{
			if (_runtimeMaterial != null)
			{
				DestroyImmediate(_runtimeMaterial);
				_runtimeMaterial = null;
			}
		}

		void CreateMaterial()
		{
			if (crtRenderSettings != null && crtRenderSettings.crtMaterial != null && _runtimeMaterial == null)
			{
				_runtimeMaterial = new Material(crtRenderSettings.crtMaterial);
			}

			if (startConfig != null)
			{
				if (!string.Equals(lastValidationId, startConfig.validationId))
				{
					lastValidationId = startConfig.validationId;
					data = startConfig.data.Clone();
				}
			}
		}

		private void Update()
		{
			CreateMaterial();
		}


		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			if (_runtimeMaterial != null && data != null)
			{
				
				
				Graphics.Blit(src, dest, _runtimeMaterial);
				return;
			}
			
			Graphics.Blit(src, dest);
		}
	}
	
	[Serializable]
	public struct ColorChannels
	{
		[Range(0, 255)]
		public int red, green, blue;

		[Tooltip("A greyscale value of 1 will completely make the image grey. A value of 0 leaves the image untouched.")]
		[Range(0, 1)] 
		public float greyScale;
	}

	[Serializable]
	public struct ScreenDimensions
	{
		[Range(0f,.5f)]
		public float width, height;
	}

	[Serializable]
	public struct ColorScan
	{
		[Range(-.5f,.5f)]
		public float greenChannelMultiplier;
		[Range(-.5f,.5f)]
		public float redBlueChannelMultiplier;
		[Range(0,10)]
		public float sizeMultiplier;
	}
}
