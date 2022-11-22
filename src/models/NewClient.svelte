<script lang="ts">
	import { newInterventionStore } from '../stores/newIntStore';
	import type { Client } from '../models/client';
	import { fade, fly } from 'svelte/transition';
	import ProgressComponent from '../components/ProgressComponent.svelte';
	let name = '';
	let surname = '';
	let address = '';
	let city = '';
	let cap = '';
	let cf = '';
	let email = '';
	let cel = '';

	function saveClient() {
		if (name === '' || surname === '') {
			alert('Compila tutti i campi');
			return;
		}
		let client: Client = {
			name,
			surname,
			address,
			city,
			cap,
			vat: cf,
			email,
			phone: cel,
			sites: []
		};
		newInterventionStore.addClient(client);
		newInterventionStore.updateStep(3);
	}
</script>

<div
	class="grid grid-cols-1 h-screen gap-10 md:grid-cols-3 "
	in:fade={{ duration: 500, delay: 600 }}
	out:fly={{ x: -200, duration: 500 }}
>
	<div class="col-span-2 ">
		<ProgressComponent />
		<div class="m-4 grid grid-flow-row scroll-auto">
			<h1>Crea Nuovo Cliente</h1>
			<div class="mt-6 flex flex-col">
				<label for="name" class="text-sm font-thin">Nome:</label>
				<input
					id="name"
					type="text"
					bind:value={name}
					class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
				/>
			</div>
			<div class="mt-6 flex flex-col">
				<label for="surname" class="text-sm font-thin">Cognome:</label>
				<input
					id="surname"
					type="text"
					bind:value={surname}
					class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
				/>
			</div>
			<div class="mt-6 flex flex-col">
				<label for="address" class="text-sm font-thin">Indirizzo:</label>
				<input
					id="address"
					type="text"
					bind:value={address}
					class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
				/>
			</div>
			<div class="flex flex-wrap">
				<div class="mt-6 flex-1 flex flex-col mr-1">
					<label for="surname" class="text-sm font-thin ">Citta':</label>
					<input
						id="city"
						type="text"
						bind:value={city}
						class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
					/>
				</div>
				<div class="mt-6 flex flex-col">
					<label for="cap" class="text-sm font-thin">Cap:</label>
					<input
						id="cap"
						type="text"
						bind:value={cap}
						class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
					/>
				</div>
			</div>
			<div class="mt-6 flex flex-col">
				<label for="cf" class="text-sm font-thin">CF/Piva:</label>
				<input
					id="cf"
					type="text"
					bind:value={cf}
					class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
				/>
			</div>
			<div class="mt-6 flex flex-col">
				<label for="cf" class="text-sm font-thin">Codice Univoco:</label>
				<input
					id="cf"
					type="text"
					bind:value={cf}
					class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
				/>
			</div>
			<div class="flex flex-wrap">
				<div class="mt-6 flex flex-col flex-1 mr-1">
					<label for="email" class="text-sm font-thin">Email:</label>
					<input
						id="email"
						type="email"
						bind:value={email}
						class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
					/>
				</div>
				<div class="mt-6 flex flex-col mr-1">
					<label for="cel" class="text-sm font-thin ">Cel:</label>
					<input
						id="cel"
						type="text"
						bind:value={cel}
						class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
					/>
				</div>
			</div>
		</div>

		<div class="m-4">
			<button
				on:click={saveClient}
				class="p-2 rounded-lg border border-slate-600 hover:text-white hover:bg-slate-600 transition-colors"
				>Salva</button
			>
			<div class="flex flex-col place-items-center items-center  md:hidden">
				<div class="responsive-smaller ">
					<img src="/images/Asset_1.png" alt="client" class="responsive-img" />
				</div>
			</div>
		</div>
	</div>
	<div class="bg-orange-300 flex-col item-center justify-end place-items-center hidden md:flex">
		<div class="mx-2 mb-10 p-6 border rounded-lg border-slate-600">
			<h1 class="p-1 text-center font-semibold">{name} {surname}</h1>
		</div>
		<div class="">
			<img src="/images/Asset_1.png" alt="client" class="responsive-img" />
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
