<template>
  <div>
    <h3>{{ t('identity.auth.device.collectionComponent.title') }}</h3>
    <p>{{ t('identity.auth.device.collectionComponent.description') }}</p>
    
    <div style="margin: 20px 0;">
      <button @click="collectDeviceInfo" :disabled="isLoading">
        {{ isLoading ? t('identity.auth.device.collectionComponent.collecting') : t('identity.auth.device.collectionComponent.collectDeviceInfo') }}
      </button>
      <button @click="clearDeviceInfo" style="margin-left: 10px;">{{ t('identity.auth.device.collectionComponent.clearInfo') }}</button>
    </div>

    <div v-if="isLoading" style="margin: 20px 0;">
      <p>{{ t('identity.auth.device.collectionComponent.collectingInfo') }}</p>
    </div>

    <div v-if="deviceInfo" style="margin: 20px 0;">
      <h4>{{ t('identity.auth.device.collectionComponent.deviceInfo') }}</h4>
      <div style="background: #f5f5f5; padding: 15px; border-radius: 5px;">
        <p><strong>{{ t('identity.auth.device.collectionComponent.deviceId') }}</strong> {{ deviceInfo.deviceId || t('identity.auth.device.collectionComponent.notGenerated') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.deviceType') }}</strong> {{ deviceInfo.deviceType || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.deviceFingerprint') }}</strong> {{ deviceInfo.loginFingerprint ? deviceInfo.loginFingerprint.substring(0, 16) + '...' : t('identity.auth.device.collectionComponent.notGenerated') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.platform') }}</strong> {{ deviceInfo.platform || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.language') }}</strong> {{ deviceInfo.language || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.timezone') }}</strong> {{ deviceInfo.timezone || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.screenResolution') }}</strong> {{ deviceInfo.screenResolution || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.colorDepth') }}</strong> {{ deviceInfo.colorDepth ? `${deviceInfo.colorDepth}${t('identity.auth.device.collectionComponent.bits')}` : t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.pixelRatio') }}</strong> {{ deviceInfo.pixelRatio || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.cpuCores') }}</strong> {{ deviceInfo.hardwareConcurrency || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.deviceMemory') }}</strong> {{ deviceInfo.deviceMemory || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.touchSupport') }}</strong> {{ deviceInfo.touchSupport || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.os') }}</strong> {{ deviceInfo.os || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.browser') }}</strong> {{ deviceInfo.browser || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.vpnDetection') }}</strong> {{ deviceInfo.isVpn || t('identity.auth.device.collectionComponent.notDetected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.proxyDetection') }}</strong> {{ deviceInfo.isProxy || t('identity.auth.device.collectionComponent.notDetected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.networkType') }}</strong> {{ deviceInfo.networkType || t('identity.auth.device.collectionComponent.notCollected') }}</p>
        <p><strong>{{ t('identity.auth.device.collectionComponent.cookieSupport') }}</strong> {{ deviceInfo.cookieEnabled ? t('identity.auth.device.collectionComponent.supported') : t('identity.auth.device.collectionComponent.notSupported') }}</p>
      </div>
      
      <div style="margin-top: 15px;">
        <button @click="copyToClipboard">{{ t('identity.auth.device.collectionComponent.copyToClipboard') }}</button>
      </div>
    </div>

    <div v-if="error" style="margin: 20px 0; color: red;">
      <p><strong>{{ t('identity.auth.device.collectionComponent.errorInfo') }}：</strong> {{ error }}</p>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';
import { getDeviceInfo } from '@/utils/loginFingerprintNativeUtil';

const { t } = useI18n();

const deviceInfo = ref(null);
const isLoading = ref(false);
const error = ref('');

const collectDeviceInfo = async () => {
  try {
    isLoading.value = true;
    error.value = '';
    
    console.log(t('identity.auth.device.collectionComponent.startCollection'));
    const info = await getDeviceInfo();
    console.log(t('identity.auth.device.collectionComponent.collectionSuccess'), info);
    
    deviceInfo.value = info;
  } catch (err) {
    console.error(t('identity.auth.device.collectionComponent.collectionFailed'), err);
    error.value = err.message || t('identity.auth.device.collectionComponent.collectionFailed');
  } finally {
    isLoading.value = false;
  }
};

const clearDeviceInfo = () => {
  deviceInfo.value = null;
  error.value = '';
};

const copyToClipboard = async () => {
  if (!deviceInfo.value) return;
  
  try {
    const text = JSON.stringify(deviceInfo.value, null, 2);
    await navigator.clipboard.writeText(text);
    alert(t('identity.auth.device.collectionComponent.copySuccess'));
  } catch (err) {
    console.error(t('identity.auth.device.collectionComponent.copyError'), err);
    alert(t('identity.auth.device.collectionComponent.copyFailed'));
  }
};

const formatTime = (timestamp) => {
  return new Date(timestamp).toLocaleString('zh-CN', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit'
  });
};
</script>
