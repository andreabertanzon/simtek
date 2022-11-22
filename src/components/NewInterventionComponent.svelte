<script lang="ts">
	import { rootStore } from '../stores/store';
	import { fly, fade } from 'svelte/transition';
	import { z } from 'zod';
	import type { Material } from '../models/material';
	let descr = '';
	let descriptions: string[] = [];

	// let workers = $rootStore.workers;
	const workerSchema = z.object({
		Name: z.string(),
		SpentHours: z.number().default(0.0),
		PriceHour: z.number().default(30.0),
		Checked: z.boolean().default(false)
	});

	const materialSchema = z.object({
		Name: z.string(),
		Pieces: z.number().positive().default(0),
		Unit: z.string().max(4).default('pz'),
		Price: z.number().positive()
	});

	type ChosenMaterial = z.infer<typeof materialSchema>;

	let workers = $rootStore.workers.map((item) => {
		try {
			let worker = workerSchema.parse(item);
			return worker;
		} catch (err) {
			console.log(err);
			return workerSchema.parse({
				Name: 'Error',
				SpentHours: 0,
				PriceHour: 0,
				Checked: false
			});
		}
	});

	let materialInput = '';
	let materials: Material[] = [];
	let chosenMaterials: ChosenMaterial[] = [];

	function filterMaterials() {
		if (materialInput.trim() === '' || materialInput.length < 3) {
			materials = [];
			return;
		}
		let matArray = materialInput.split(' ');
		if (matArray.length <= 1) {
			materials = $rootStore.materials.filter((item) =>
				item.Name.toLowerCase().includes(materialInput.trim().toLowerCase())
			);
			return;
		}

		materials = $rootStore.materials
			.filter((item) => item.Name.toLowerCase().match(matArray[0].trim().toLowerCase()))
			.filter((item) => item.Name.toLowerCase().match(matArray[1].trim().toLowerCase()));
	}

	$: materialInput && filterMaterials();

	$: getPriceTotal = () => {
		if (workers.filter((item) => item.Checked).length === 0) return 0;
		let result = workers
			.filter((item) => item.Checked)
			.map((item) => item.PriceHour * item.SpentHours)
			.reduce((sum, a) => sum + a);

		console.log(`GETPRICE: ${result}`);

		return result === undefined ? 0 : result;
	};

	$: getHourTotal = () => {
		if (workers.filter((item) => item.Checked).length === 0) return 0;
		let result = workers
			.filter((item) => item.Checked)
			.map((item) => item.SpentHours)
			.reduce((sum, a) => sum + a);
		console.log(`GETHOURS: ${result}`);
		return result === undefined ? 0 : result;
	};

	function addDescription(e: KeyboardEvent) {
		if (e.key === 'Enter') {
			descriptions = [...descriptions, descr];
			descr = '';
		}
	}
	function removeDescr(i: number) {
		descriptions = descriptions.filter((_, idx) => idx !== i);
	}
	function removeMaterial(i: number) {
		chosenMaterials = chosenMaterials.filter((_, idx) => idx !== i);
	}

	function addMaterial(m: Material) {
		try {
			let result = {
				Name: m.Name,
				Pieces: 0,
				Price: m.Price,
				Unit: m.Unit
			} as ChosenMaterial;
			chosenMaterials = [...chosenMaterials, result];
			materialInput = '';
			materials = [];
		} catch (err) {
			console.log(err);
		}
	}

	function saveIntervention() {
		console.log('saved');
	}
</script>

<div
	class="grid grid-cols-1 h-screen gap-2 md:grid-cols-3"
	in:fade={{ duration: 500, delay: 600 }}
	out:fly={{ x: -200, duration: 500 }}
>
	<div class="col-span-2 overflow-y-scroll">
		<div class="m-4 grid grid-flow-row scroll-auto">
			<h1>Definisci Intervento</h1>

			<div class="mt-8 grid grid-flow-row rounded-md border {workers.length === 0 ? 'hidden' : ''}">
				<div class="overflow-y-auto scroll-auto max-h-96">
					{#each workers as worker}
						<!-- svelte-ignore a11y-click-events-have-key-events -->
						<div
							on:click={() => ''}
							class="p-2 border cursor-pointer transion-colors 
 					 flex flex-row justify-between"
						>
							<div class="flex self-center h-6 w-6">
								<input id="checked-checkbox" type="checkbox" bind:checked={worker.Checked} />
							</div>
							<div class="flex flex-column">
								<p class="text-sm font-normal flex-grow self-center">{worker.Name}</p>
							</div>
							<div class="flex flex-col">
								<label for="hours" class="text-xs font-thin">ore spese:</label>
								<input
									bind:value={worker.SpentHours}
									id="hours"
									type="number"
									class="text-sm font-normal border rounded-md px-1 py-0.5"
								/>
							</div>
							<div class="flex flex-col">
								<label for="hours" class="text-xs font-thin">prezzo:</label>
								<input
									bind:value={worker.PriceHour}
									id="euro"
									type="number"
									class="text-sm font-normal border rounded-md px-1 py-0.5"
								/>
							</div>
						</div>
					{/each}
				</div>
			</div>

			<div class="mt-6 flex flex-col">
				<label for="name" class="text-sm font-thin">Breve titolo:</label>
				<input
					id="name"
					type="text"
					class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
				/>
			</div>
			<div class="mt-6 flex flex-col">
				<div
					class="max-h-28 {descriptions.length > 0
						? `border border-slate-100 rounded-md p-1 scroll-m-0 mb-1`
						: ``} scroll-auto overflow-y-scroll"
				>
					{#each descriptions as des, i}
						<div class="flex flex-row items-center mb-2">
							<!-- svelte-ignore a11y-click-events-have-key-events -->
							<svg
								on:click={() => removeDescr(i)}
								xmlns="http://www.w3.org/2000/svg"
								viewBox="0 0 24 24"
								fill="currentColor"
								class="w-4 h-4 shrink-0"
							>
								<path
									fill-rule="evenodd"
									d="M12 2.25c-5.385 0-9.75 4.365-9.75 9.75s4.365 9.75 9.75 9.75 9.75-4.365 9.75-9.75S17.385 2.25 12 2.25zm-1.72 6.97a.75.75 0 10-1.06 1.06L10.94 12l-1.72 1.72a.75.75 0 101.06 1.06L12 13.06l1.72 1.72a.75.75 0 101.06-1.06L13.06 12l1.72-1.72a.75.75 0 10-1.06-1.06L12 10.94l-1.72-1.72z"
									clip-rule="evenodd"
								/>
							</svg>

							<p class="ml-1 text-black text-sm font-thin break-words">
								{des}
							</p>
						</div>
					{/each}
				</div>
				<label for="surname" class="text-sm font-thin">Descrizione:</label>
				<input
					bind:value={descr}
					on:keypress={addDescription}
					id="surname"
					type="text"
					class="border border-slate-600 rounded-lg font-normal p-2 text-sm"
				/>
			</div>
		</div>

		<label for="materials" class="mx-4 text-sm font-thin">Materiali:</label>

		<div
			class="flex flex-col mx-4 max-h-28 {descriptions.length > 0
				? `border border-slate-100 rounded-md p-1 scroll-m-0 mb-1`
				: ``} scroll-auto overflow-y-scroll"
		>
			{#each chosenMaterials as chMat, i}
				<div class="flex flex-row items-center mb-2 justify-start">
					<!-- svelte-ignore a11y-click-events-have-key-events -->
					<svg
						on:click={() => removeMaterial(i)}
						xmlns="http://www.w3.org/2000/svg"
						viewBox="0 0 24 24"
						fill="currentColor"
						class="w-4 h-4 shrink-0"
					>
						<path
							fill-rule="evenodd"
							d="M12 2.25c-5.385 0-9.75 4.365-9.75 9.75s4.365 9.75 9.75 9.75 9.75-4.365 9.75-9.75S17.385 2.25 12 2.25zm-1.72 6.97a.75.75 0 10-1.06 1.06L10.94 12l-1.72 1.72a.75.75 0 101.06 1.06L12 13.06l1.72 1.72a.75.75 0 101.06-1.06L13.06 12l1.72-1.72a.75.75 0 10-1.06-1.06L12 10.94l-1.72-1.72z"
							clip-rule="evenodd"
						/>
					</svg>
					<div class="flex flex-row mr-4">
						<p class="ml-1 text-black text-sm font-thin break-words">
							{chMat.Name},
						</p>
					</div>
					<label for="pieces" class="font-thin text-sm mr-2">{chMat.Unit}:</label>
					<input
						bind:value={chMat.Pieces}
						id="pieces"
						type="number"
						class="text-sm border font-thin border-slate-100 rounded-md p-0.5 w-10 mr-2"
					/>
					<label for="matPrice" class="font-thin text-sm mr-2">Price:</label>
					<input
						bind:value={chMat.Price}
						id="matPrice"
						type="number"
						class="text-sm font-thin border border-slate-100 rounded-md p-0.5 w-10"
					/>
				</div>
			{/each}
		</div>
		<div class="mx-4 grid grid-flow-row rounded-md border">
			<input
				bind:value={materialInput}
				id="materials"
				type="text"
				class="pl-2 py-1 {materials.length == 0
					? `rounded-md`
					: `rounded-t-md`} border border-slate-600 text-sm font-normal"
			/>
			<div class="overflow-y-auto scroll-auto max-h-96">
				{#each materials as material}
					<!-- svelte-ignore a11y-click-events-have-key-events -->
					<div
						class="p-4 border cursor-pointer transion-colors hover:bg-[#afb39a]
 				 flex justify-between"
						on:click={() => addMaterial(material)}
					>
						<p class="text-sm font-normal">{material.Name}</p>
					</div>
				{/each}
			</div>
		</div>
		<div class="flex flex-col">
			<label for="notes" class="text-sm font-thin mx-4 mt-4">Note: </label>
			<textarea
				id="notes"
				type="text"
				rows="5"
				class="flex-1 border border-slate-600 rounded-md mx-4 my-4 text-sm font-normal p-2"
			/>
		</div>

		<div class="m-4">
			<button
				on:click={saveIntervention}
				class="p-2 px-4 rounded-full border border-slate-600 hover:text-white hover:bg-slate-600 transition-colors"
				>Salva</button
			>
		</div>
	</div>
	<div class="h-screen overflow-y-scroll bg-slate-100 p-2 hidden md:flex md:flex-col">
		<h2 class="text-sm">Lavoratori:</h2>
		<div class="flex my-4">
			<table class="table-fixed flex-1">
				<tbody>
					{#each workers.filter((item) => item.Checked) as worker}
						<tr class="border border-slate-900">
							<td><p class="font-normal text-sm">{worker.Name}</p></td>
							<td><p class="font-normal text-sm">{worker.SpentHours}h</p></td>
							<td><p class="font-normal text-sm">{worker.PriceHour * worker.SpentHours}€</p></td>
						</tr>
					{/each}
					<tr class="border border-slate-900">
						<td><p class="font-bold text-sm">Totale</p></td>
						<td><p class="font-bold text-sm">{getHourTotal()}h</p></td>
						<td><p class="font-bold text-sm">{getPriceTotal()}€</p></td>
					</tr>
				</tbody>
			</table>
		</div>
		<h2 class="text-sm">Descrizione:</h2>
		<div class="flex">
			<table class="table-fixed  flex-1">
				<tbody>
					{#each descriptions as des}
						<tr class="border border-slate-900"
							><td><p class="font-normal text-sm">{des}</p></td></tr
						>
					{/each}
				</tbody>
			</table>
		</div>
		<h2 class="text-sm mt-2">Materiali:</h2>
		<div class="flex">
			<table class="table-fixed  flex-1">
				<tbody>
					{#each chosenMaterials as mat}
						<tr class="border border-slate-900">
							<td><p class="font-normal text-sm">{mat.Name}</p></td>
							<td><p class="font-normal text-sm">{mat.Price * mat.Pieces}€</p></td>
						</tr>
					{/each}
				</tbody>
			</table>
		</div>
	</div>
	<div />
</div>
