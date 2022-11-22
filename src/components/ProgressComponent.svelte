<script lang="ts">
	import { newInterventionStore } from '../stores/newIntStore';
	$newInterventionStore;

	function clickStep(n: 1 | 3 | 4) {
		if ($newInterventionStore.step >= n) {
			newInterventionStore.updateStep(n);
		}
	}
</script>

<div class="my-10 flex justify-center ">
	<!-- svelte-ignore a11y-click-events-have-key-events -->
	<div
		on:click={() => clickStep(1)}
		class="rounded-full {$newInterventionStore.step >= 1
			? 'bg-[#afb39a]'
			: 'bg-gray-200'} w-6 h-6 mx-8 cursor-pointer"
	/>
	<!-- svelte-ignore a11y-click-events-have-key-events -->
	<div
		on:click={() => clickStep(3)}
		class="rounded-full 
		{$newInterventionStore.step >= 3 ? 'bg-[#afb39a]' : 'bg-gray-200'}
		w-6 h-6 mx-8 cursor-pointer "
	/>
	<!-- svelte-ignore a11y-click-events-have-key-events -->
	<div
		on:click={() => clickStep(4)}
		class="rounded-full {$newInterventionStore.step >= 4
			? 'bg-[#afb39a]'
			: 'bg-gray-200'} w-6 h-6 mx-8 cursor-pointer"
	/>
</div>
