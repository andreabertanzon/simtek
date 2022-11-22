<script lang="ts">
	import { rootStore } from '../stores/store';
	import { newInterventionStore } from '../stores/newIntStore';
	import { fade, fly } from 'svelte/transition';
	import ProgressComponent from '../components/ProgressComponent.svelte';
	import { type Site, siteSchema } from '../models/site';

	let currentSite: Site = {
		Name: '',
		City: undefined,
		Cap: undefined,
		Address: undefined
	};

	const sitesCall = $rootStore.clients.find(
		(item) =>
			item.name === $newInterventionStore.choosenClient?.name &&
			item.surname === $newInterventionStore.choosenClient?.surname
	)?.sites as Site[];

	const sites = sitesCall === undefined ? [] : sitesCall;
	let createClicked = false;

	function submitSite() {
		if (!siteSchema.safeParse(currentSite).success) {
			alert(`Nome cantiere obbligatorio ${currentSite.Name}`);
			return;
		}
		newInterventionStore.updateSite(siteSchema.parse(currentSite));
		newInterventionStore.updateStep(4);
	}

	function setCurrentSite(s: Site) {
		currentSite = s;
	}
</script>

<div
	class="grid grid-cols-1 h-screen gap-10 md:grid-cols-3 relative md:overflow-hidden"
	in:fade={{ duration: 500, delay: 600 }}
	out:fly={{ x: -200, duration: 500 }}
>
	<div class="col-span-2 {createClicked ? `hidden` : ``}">
		<div class="my-4 ml-4">
			<ProgressComponent />
			<h1>Scegli Cantiere:</h1>
			<div
				class="mt-8 grid grid-flow-row rounded-md border shadow-lg {sites.length === 0
					? 'hidden'
					: ''}"
			>
				<input
					type="text"
					class="pl-2 py-1 rounded-t-md border border-slate-300 text-normal font-normal"
				/>
				<div class="overflow-y-auto scroll-auto max-h-96">
					{#each sites as site}
						<!-- svelte-ignore a11y-click-events-have-key-events -->
						<div
							on:click={() => setCurrentSite(site)}
							class="p-4 border cursor-pointer transion-colors hover:bg-[#afb39a]
 					{currentSite === site ? 'bg-[#afb39a] text-white' : 'border-slate-100'} flex justify-between"
						>
							<p class="text-sm font-normal">{site.Name}</p>
						</div>
					{/each}
				</div>
			</div>
			<div class="mt-2">
				<p class="font-normal">
					Non lo trovi?
					<!-- svelte-ignore a11y-click-events-have-key-events -->
					<span class="text-orange-400 cursor-pointer" on:click={() => (createClicked = true)}
						>Crealo</span
					>
				</p>
			</div>
		</div>
		<div class="m-4">
			<button
				on:click={() => submitSite()}
				class="{sites.length === 0
					? 'hidden'
					: ''} p-2 rounded-lg border border-slate-600 hover:text-white hover:bg-slate-600 transition-colors"
				>Prosegui</button
			>
		</div>
	</div>
	<div class="col-span-2 ml-4 {createClicked ? `` : `hidden`} flex flex-col">
		<ProgressComponent />
		<form on:submit|preventDefault={submitSite}>
			<div class="flex flex-col">
				<label for="siteName" class="text-sm font-thin ">Nome Cantiere</label>
				<input
					bind:value={currentSite.Name}
					id="siteName"
					type="text"
					class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
				/>
			</div>
			<div class="mt-6 flex flex-col">
				<label for="address" class="text-sm font-thin">Indirizzo:</label>
				<input
					id="address"
					type="text"
					bind:value={currentSite.Address}
					class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
				/>
			</div>
			<div class="flex flex-wrap">
				<div class="mt-6 flex-1 flex flex-col mr-1">
					<label for="surname" class="text-sm font-thin ">Citta':</label>
					<input
						id="city"
						type="text"
						bind:value={currentSite.City}
						class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
					/>
				</div>
				<div class="mt-6 flex flex-col">
					<label for="cap" class="text-sm font-thin">Cap:</label>
					<input
						id="cap"
						type="text"
						bind:value={currentSite.Cap}
						class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
					/>
				</div>
			</div>
			<div class="mt-4">
				<button
					type="submit"
					class="p-2 rounded-lg border border-slate-600 hover:text-white hover:bg-slate-600 transition-colors"
					>Salva</button
				>
			</div>
		</form>
	</div>

	<div
		class="bg-[#f1f5f9] flex-col justify-end md:justify-start  hidden md:flex h-screen overflow-x-hidden"
	>
		<div class="overflow-x-hidden self-center">
			<img src="/images/Asset_5.png" alt="client" class="responsive-img" />
		</div>
		<div class="mx-2 lg:mx-6 mt-20 p-2 border rounded-lg border-slate-600 grid-flow-row">
			<p class="font-normal self-center mb-2">
				{$newInterventionStore.choosenClient === null ||
				$newInterventionStore.choosenClient === undefined
					? 'Seleziona cliente'
					: `${$newInterventionStore.choosenClient?.name} ${$newInterventionStore.choosenClient?.surname}`}:
			</p>
			<div class="flex flex-wrap">
				<div class="flex flex-nowrap mr-2 my-2">
					<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="black" class="w-4 h-4">
						<path
							fill-rule="evenodd"
							d="M7.5 6a4.5 4.5 0 119 0 4.5 4.5 0 01-9 0zM3.751 20.105a8.25 8.25 0 0116.498 0 .75.75 0 01-.437.695A18.683 18.683 0 0112 22.5c-2.786 0-5.433-.608-7.812-1.7a.75.75 0 01-.437-.695z"
							clip-rule="evenodd"
						/>
					</svg>
					<p class="font-thin text-sm ml-2">
						{$newInterventionStore.choosenClient?.email === ''
							? `non.trovata@email.com`
							: $newInterventionStore.choosenClient?.email}
					</p>
				</div>
				<div class="flex flex-nowrap mr-2 my-2">
					<svg
						xmlns="http://www.w3.org/2000/svg"
						viewBox="0 0 24 24"
						fill="currentColor"
						class="w-4 h-4"
					>
						<path
							d="M5.223 2.25c-.497 0-.974.198-1.325.55l-1.3 1.298A3.75 3.75 0 007.5 9.75c.627.47 1.406.75 2.25.75.844 0 1.624-.28 2.25-.75.626.47 1.406.75 2.25.75.844 0 1.623-.28 2.25-.75a3.75 3.75 0 004.902-5.652l-1.3-1.299a1.875 1.875 0 00-1.325-.549H5.223z"
						/>
						<path
							fill-rule="evenodd"
							d="M3 20.25v-8.755c1.42.674 3.08.673 4.5 0A5.234 5.234 0 009.75 12c.804 0 1.568-.182 2.25-.506a5.234 5.234 0 002.25.506c.804 0 1.567-.182 2.25-.506 1.42.674 3.08.675 4.5.001v8.755h.75a.75.75 0 010 1.5H2.25a.75.75 0 010-1.5H3zm3-6a.75.75 0 01.75-.75h3a.75.75 0 01.75.75v3a.75.75 0 01-.75.75h-3a.75.75 0 01-.75-.75v-3zm8.25-.75a.75.75 0 00-.75.75v5.25c0 .414.336.75.75.75h3a.75.75 0 00.75-.75v-5.25a.75.75 0 00-.75-.75h-3z"
							clip-rule="evenodd"
						/>
					</svg>

					<p class="font-thin text-sm ml-2">
						{currentSite.Name === undefined || currentSite.Name === ''
							? 'Nome Cantiere'
							: `${currentSite.Name}`}
					</p>
				</div>
				<div class="flex flex-nowrap mr-2 my-2">
					<svg
						xmlns="http://www.w3.org/2000/svg"
						viewBox="0 0 24 24"
						fill="currentColor"
						class="w-4 h-4"
					>
						<path
							d="M5.223 2.25c-.497 0-.974.198-1.325.55l-1.3 1.298A3.75 3.75 0 007.5 9.75c.627.47 1.406.75 2.25.75.844 0 1.624-.28 2.25-.75.626.47 1.406.75 2.25.75.844 0 1.623-.28 2.25-.75a3.75 3.75 0 004.902-5.652l-1.3-1.299a1.875 1.875 0 00-1.325-.549H5.223z"
						/>
						<path
							fill-rule="evenodd"
							d="M3 20.25v-8.755c1.42.674 3.08.673 4.5 0A5.234 5.234 0 009.75 12c.804 0 1.568-.182 2.25-.506a5.234 5.234 0 002.25.506c.804 0 1.567-.182 2.25-.506 1.42.674 3.08.675 4.5.001v8.755h.75a.75.75 0 010 1.5H2.25a.75.75 0 010-1.5H3zm3-6a.75.75 0 01.75-.75h3a.75.75 0 01.75.75v3a.75.75 0 01-.75.75h-3a.75.75 0 01-.75-.75v-3zm8.25-.75a.75.75 0 00-.75.75v5.25c0 .414.336.75.75.75h3a.75.75 0 00.75-.75v-5.25a.75.75 0 00-.75-.75h-3z"
							clip-rule="evenodd"
						/>
					</svg>
					<p class="font-thin text-sm ml-2">
						{currentSite.City === undefined ? 'Citta del cantiere' : `${currentSite.City}`}
					</p>
				</div>
				<div class="flex flex-nowrap mr-2 my-2">
					<svg
						xmlns="http://www.w3.org/2000/svg"
						viewBox="0 0 24 24"
						fill="currentColor"
						class="w-4 h-4"
					>
						<path
							d="M5.223 2.25c-.497 0-.974.198-1.325.55l-1.3 1.298A3.75 3.75 0 007.5 9.75c.627.47 1.406.75 2.25.75.844 0 1.624-.28 2.25-.75.626.47 1.406.75 2.25.75.844 0 1.623-.28 2.25-.75a3.75 3.75 0 004.902-5.652l-1.3-1.299a1.875 1.875 0 00-1.325-.549H5.223z"
						/>
						<path
							fill-rule="evenodd"
							d="M3 20.25v-8.755c1.42.674 3.08.673 4.5 0A5.234 5.234 0 009.75 12c.804 0 1.568-.182 2.25-.506a5.234 5.234 0 002.25.506c.804 0 1.567-.182 2.25-.506 1.42.674 3.08.675 4.5.001v8.755h.75a.75.75 0 010 1.5H2.25a.75.75 0 010-1.5H3zm3-6a.75.75 0 01.75-.75h3a.75.75 0 01.75.75v3a.75.75 0 01-.75.75h-3a.75.75 0 01-.75-.75v-3zm8.25-.75a.75.75 0 00-.75.75v5.25c0 .414.336.75.75.75h3a.75.75 0 00.75-.75v-5.25a.75.75 0 00-.75-.75h-3z"
							clip-rule="evenodd"
						/>
					</svg>
					<p class="font-thin text-sm ml-2">
						{currentSite.Cap === undefined ? 'Cap del cantiere' : `${currentSite.Cap}`}
					</p>
				</div>
				<div class="flex flex-nowrap mr-2 my-2">
					<svg
						xmlns="http://www.w3.org/2000/svg"
						viewBox="0 0 24 24"
						fill="currentColor"
						class="w-4 h-4"
					>
						<path
							d="M5.223 2.25c-.497 0-.974.198-1.325.55l-1.3 1.298A3.75 3.75 0 007.5 9.75c.627.47 1.406.75 2.25.75.844 0 1.624-.28 2.25-.75.626.47 1.406.75 2.25.75.844 0 1.623-.28 2.25-.75a3.75 3.75 0 004.902-5.652l-1.3-1.299a1.875 1.875 0 00-1.325-.549H5.223z"
						/>
						<path
							fill-rule="evenodd"
							d="M3 20.25v-8.755c1.42.674 3.08.673 4.5 0A5.234 5.234 0 009.75 12c.804 0 1.568-.182 2.25-.506a5.234 5.234 0 002.25.506c.804 0 1.567-.182 2.25-.506 1.42.674 3.08.675 4.5.001v8.755h.75a.75.75 0 010 1.5H2.25a.75.75 0 010-1.5H3zm3-6a.75.75 0 01.75-.75h3a.75.75 0 01.75.75v3a.75.75 0 01-.75.75h-3a.75.75 0 01-.75-.75v-3zm8.25-.75a.75.75 0 00-.75.75v5.25c0 .414.336.75.75.75h3a.75.75 0 00.75-.75v-5.25a.75.75 0 00-.75-.75h-3z"
							clip-rule="evenodd"
						/>
					</svg>
					<p class="font-thin text-sm ml-2">
						{currentSite.Address === undefined ? 'Via del cantiere' : `${currentSite.Address}`}
					</p>
				</div>
			</div>
		</div>
	</div>
	<div class="m-4">
		<div
			class="flex flex-col place-items-center items-center overflow-x-hidden justify-center align-middle md:hidden"
		>
			<div class="responsive-smaller ">
				<img src="/images/Asset_5.png" alt="client" class="responsive-img" />
			</div>
		</div>
	</div>
</div>

<style>
	.responsive-img {
		width: 100%;
		min-width: 300px;
		height: 100%;
	}
	.responsive-smaller {
		width: auto;
		height: auto;
	}
</style>
