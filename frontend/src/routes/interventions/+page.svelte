<script lang="ts">
	import SearchComponent from "../../components/SearchComponent.svelte";
	import type { Intervention } from "src/models/intervention";
	import CardComponent from "../../components/CardComponent.svelte";
	import { fly } from "svelte/transition";
	import { rootStore } from "../../stores/store";
	// export let data;

	let interventions = $rootStore.interventions;
	let filteredInterventions: Intervention[] = interventions;

	function filterInterventions(filtered: Intervention[]) {
		filteredInterventions = filtered;
	}
</script>

<section
	in:fly={{ y: 50, duration: 500, delay: 500 }}
	out:fly={{ duration: 500 }}
>
	<div class="flex flex-col p-10">
		<SearchComponent
			interventions={filteredInterventions}
			callback={filterInterventions}
			refresh={() => (filteredInterventions = interventions)}
		/>
		<div class="popular-movies">
			{#each filteredInterventions as intv}
				<CardComponent currentIntervention={intv} />
			{/each}
		</div>
	</div>
</section>

<style>
	.popular-movies {
		display: grid;
		margin: 1rem;
		grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
		column-gap: 1rem;
		row-gap: 2rem;
	}
</style>
