#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
	StructuredBuffer<float4x4> _Matrices;
#endif

float _Step;

void ConfigureProcedural() {
	#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
		unity_ObjectToWorld = _Matrices[unity_InstanceID];
	#endif
}

// dummy functions to call from the shader graph so that this file can be included via a custom node
// 2 versions for both precissions of shder graph (suffix _float is important)
void ShaderGraphFunction_float(float3 In, out float3 Out) {
	Out = In;
}

void ShaderGraphFunction_half(half3 In, out half3 Out) {
	Out = In;
}