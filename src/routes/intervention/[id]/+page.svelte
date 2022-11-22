<script lang="ts">
	import { newInterventionStore } from '../../../stores/newIntStore';
	import type { Client } from '../../../models/client';
	import { fade, fly } from 'svelte/transition';
	import type { intervention } from '../../../models/intervention';
	import ChooseClient from '../../../components/ChooseClient.svelte';
	import NewClient from '../../../models/NewClient.svelte';
	import NewSiteComponent from '../../../components/NewSiteComponent.svelte';
	import NewInterventionComponent from '../../../components/NewInterventionComponent.svelte';
</script>

{#if $newInterventionStore.step == 1}
	<div
		class={$newInterventionStore.step !== 1 ? 'hidden' : ''}
		transition:fly={{ x: -200, duration: 500 }}
	>
		<ChooseClient />
	</div>
{/if}
{#if $newInterventionStore.step == 2}
	<div transition:fly={{ x: -200, duration: 500 }}>
		<NewClient />
	</div>
{/if}
{#if $newInterventionStore.step == 3}
	<div in:fade={{ duration: 500, delay: 600 }} out:fly={{ x: -200, duration: 500 }}>
		<NewSiteComponent />
	</div>
{/if}
{#if $newInterventionStore.step == 4}
	<NewInterventionComponent />
{/if}
