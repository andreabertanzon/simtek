<script lang="ts">
	export let data;
	import { fly } from 'svelte/transition';
	let interventions = data.interventions;
	console.log(interventions);
</script>

<section in:fly={{ y: 50, duration: 500, delay: 500 }} out:fly={{ duration: 500 }}>
	<div class="grid grid-cols-3 h-screen">
		<div class="col-span-2 p-10">
			<h1>Ciao, Simone!</h1>
			<div class="grid grid-cols-2 mt-10 gap gap-4">
				<div class="bg-orange-900 shadow-slate-300 shadow-lg h-20 rounded-lg items-center">
					<p class="text-center p-6 text-white">Hello</p>
				</div>
				<div class="bg-[#868a73] h-20 rounded-lg">
					<p class="text-center p-6 text-white">World</p>
				</div>
				<div />
			</div>
		</div>
		<div class="bg-[#868a73] hidden md:block " />
	</div>
</section>
