<script lang="ts">
	import type { Client } from "../models/client";
	import { rootStore } from "../stores/store";
	import { newInterventionStore } from "../stores/newIntStore";
	import { fade, fly } from "svelte/transition";
	import ProgressComponent from "./ProgressComponent.svelte";
	$newInterventionStore.choosenClient;
	let clientSearch = "";
	let clients = $rootStore.clients;

	function filterClients(text: string): Client[] {
		let splittedText = text.trimStart().trimEnd().split(" ");
		if (splittedText.length == 1) {
			return text === ""
				? $rootStore.clients
				: clients.filter(
						(item) =>
							item.name.includes(text.trim()) || item.surname.includes(text)
				  );
		} else {
			return clients.filter(
				(item) =>
					item.name.includes(splittedText[0].trim()) &&
					item.surname.includes(splittedText[1].trim())
			);
		}
	}

	function clickClient(c: Client) {
		newInterventionStore.addClient(c);
	}

	function incrementStep() {
		if ($newInterventionStore.choosenClient === null) {
			alert("Seleziona un cliente!");
			return;
		}
		newInterventionStore.updateStep(3);
	}

	$: clientSearch && filterClients(clientSearch);
	$: filteredClients = filterClients(clientSearch);
	$: hiddenClass = filteredClients.length > 0 ? "hidden" : "";
</script>

<div
	class="grid grid-cols-1 h-screen gap-10 md:grid-cols-3 relative md:overflow-hidden"
	in:fade={{ duration: 500, delay: 600 }}
	out:fly={{ x: -200, duration: 500 }}
>
	<div class="col-span-2 ">
		<div class="my-4 ml-4">
			<ProgressComponent />
			<h1>Cerca Cliente:</h1>
			<div class="mt-8 grid grid-flow-row rounded-md border shadow-lg">
				<input
					type="text"
					bind:value={clientSearch}
					class="pl-2 py-1 rounded-t-md border border-slate-300 text-normal font-normal"
				/>
				<div class="overflow-y-auto scroll-auto max-h-64">
					{#each filteredClients as client}
						<!-- svelte-ignore a11y-click-events-have-key-events -->
						<div
							class="p-4 border cursor-pointer transion-colors hover:bg-[#afb39a]
 					{$newInterventionStore.choosenClient === client
								? 'bg-[#afb39a] text-white'
								: 'border-slate-100'} flex justify-between"
							on:click={() => clickClient(client)}
						>
							<p class="text-sm font-normal">{client.name} {client.surname}</p>
						</div>
					{/each}
				</div>
			</div>
			<div class="mt-2">
				<p class="font-normal {hiddenClass}">
					Non lo trovi?
					<!-- svelte-ignore a11y-click-events-have-key-events -->
					<span
						class="text-orange-400 cursor-pointer"
						on:click={() => newInterventionStore.updateStep(2)}>Crealo</span
					>
				</p>
			</div>
		</div>
		<div class="m-4">
			<button
				on:click={incrementStep}
				class="{filteredClients.length === 0
					? 'hidden'
					: ''} p-2 rounded-lg border border-slate-600 hover:text-white hover:bg-slate-600 transition-colors"
				>Prosegui</button
			>
		</div>
	</div>
	<div
		class="bg-orange-300 flex-col item-center justify-end place-items-center hidden md:flex h-screen"
	>
		<div class="mx-2 mb-10 p-6 border rounded-lg border-slate-600">
			{$newInterventionStore.choosenClient === null ||
			$newInterventionStore.choosenClient === undefined
				? "Seleziona un cliente"
				: `${$newInterventionStore.choosenClient?.name} ${$newInterventionStore.choosenClient?.surname}`}
		</div>
		<div class="">
			<img src="/images/Asset_1.png" alt="client" class="responsive-img" />
		</div>
	</div>
	<div class="m-4">
		<div class="flex flex-col place-items-center items-center  md:hidden">
			<div class="responsive-smaller ">
				<img src="/images/Asset_1.png" alt="client" class="responsive-img" />
			</div>
		</div>
	</div>
</div>

<style>
	.responsive-img {
		width: 100%;
		max-width: 400px;
		height: 100%;
	}
	.responsive-smaller {
		width: 100%;
		height: 100%;
	}
</style>
