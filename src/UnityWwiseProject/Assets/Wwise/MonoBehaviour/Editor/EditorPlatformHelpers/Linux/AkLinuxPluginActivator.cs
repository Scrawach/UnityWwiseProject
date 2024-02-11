/*******************************************************************************
The content of this file includes portions of the proprietary AUDIOKINETIC Wwise
Technology released in source code form as part of the game integration package.
The content of this file may not be used without valid licenses to the
AUDIOKINETIC Wwise Technology.
Note that the use of the game engine is subject to the Unity(R) Terms of
Service at https://unity3d.com/legal/terms-of-service
 
License Usage
 
Licensees holding valid licenses to the AUDIOKINETIC Wwise Technology may use
this file in accordance with the end user license agreement provided with the
software or, alternatively, in accordance with the terms contained
in a written agreement between you and Audiokinetic Inc.
Copyright (c) 2024 Audiokinetic Inc.
*******************************************************************************/

﻿#if UNITY_EDITOR
[UnityEditor.InitializeOnLoad]
public class AkLinuxPluginActivator
{
	static AkLinuxPluginActivator()
	{
		if (UnityEditor.AssetDatabase.IsAssetImportWorkerProcess())
		{
			return;
		}

#if !UNITY_2019_2_OR_NEWER
		AkPluginActivator.RegisterBuildTarget(UnityEditor.BuildTarget.StandaloneLinuxUniversal, new AkPluginActivator.PlatformConfiguration
		{
			WwisePlatformName = "Linux",
			PluginDirectoryName = "Linux"
		});
		AkBuildPreprocessor.RegisterBuildTarget(UnityEditor.BuildTarget.StandaloneLinuxUniversal, new AkBuildPreprocessor.PlatformConfiguration
		{
			WwisePlatformName = "Linux"
		});
#endif
		AkPluginActivator.RegisterBuildTarget(UnityEditor.BuildTarget.StandaloneLinux64, new AkPluginActivator.PlatformConfiguration
		{
			WwisePlatformName = "Linux",
			PluginDirectoryName = "Linux"
		});
		AkBuildPreprocessor.RegisterBuildTarget(UnityEditor.BuildTarget.StandaloneLinux64, new AkBuildPreprocessor.PlatformConfiguration
		{
			WwisePlatformName = "Linux"
		});
		WwiseSetupWizard.AddBuildTargetGroup(UnityEditor.BuildTargetGroup.Standalone);
	}
}
#endif