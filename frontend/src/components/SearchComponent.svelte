<script lang="ts">
	import type { intervention } from 'src/models/intervention';
	export let interventions: intervention[];
	export let callback: (a: intervention[]) => void;
	export let refresh: () => void;
	let inputValue = '';
	let active = false;
	function filterMovies() {
		console.log(inputValue);
		interventions = interventions.filter((item) => item.date.indexOf(inputValue) !== -1);
		callback(interventions);
		console.log(interventions.length);
		inputValue = '';
	}
	import { fly } from 'svelte/transition';
</script>

<div class="search-form flex flex-col pr-8">
	<form class="search" on:submit|preventDefault={filterMovies}>
		{#if !active}
			<label
				in:fly={{ y: -10, duration: 500 }}
				out:fly={{ y: -10, duration: 500 }}
				for="input-search">Cerca</label
			>
		{/if}
		<input
			on:focus={() => (active = true)}
			on:blur={() => (inputValue === '' ? (active = false) : (active = true))}
			bind:value={inputValue}
			name="input-search"
			type="text"
			class={active ? 'selected' : ''}
		/>
		{#if inputValue}
			<button in:fly={{ y: 0, duration: 500 }} out:fly={{ y: 0, duration: 500 }}>
				<svg
					xmlns="http://www.w3.org/2000/svg"
					fill="none"
					viewBox="0 0 24 24"
					stroke-width="1.5"
					stroke="currentColor"
					class="w-6 h-6"
				>
					<path
						stroke-linecap="round"
						stroke-linejoin="round"
						d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z"
					/>
				</svg>
			</button>
		{/if}
		{#if !inputValue}
			<button in:fly={{ y: 0, duration: 200 }} out:fly={{ y: 0, duration: 200 }} on:click={refresh}>
				<svg
					xmlns="http://www.w3.org/2000/svg"
					fill="none"
					viewBox="0 0 24 24"
					stroke-width="1.5"
					stroke="white"
					class="w-6 h-6"
				>
					<path
						stroke-linecap="round"
						stroke-linejoin="round"
						d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z"
					/>
				</svg>
			</button>
		{/if}
	</form>
</div>

<style>
	.search-form {
		display: flex;
	}
	.search {
		position: relative;
		width: 100%;
		margin: 1rem;
	}
	button {
		font-size: 0.7rem;
		padding: 0rem 1rem;
		background: orange;
		color: white;
		font-weight: bold;
		border: none;
		position: absolute;
		bottom: 50%;
		right: 0;
		transform: translate(0, 50%);
		height: 100%;
		border-top-right-radius: 10px;
		border-bottom-right-radius: 10px;
		cursor: pointer;
	}
	input {
		width: 100%;
		border: none;
		font-size: 1rem;
		outline: none;
		color: rgb(255, 255, 255);
		padding: 0.5rem 0.1rem;
		transition: background 0.75s ease-out;
		font-weight: bold;
		background: rgb(63, 63, 63);
		border-radius: 10px;
		padding: 1rem;
	}
	label {
		position: absolute;
		font-size: 0.8rem;
		top: 50%;
		left: 0;
		transform: translate(0, -50%);
		pointer-events: none;
		color: #fff;
		padding: 0rem 1rem;
	}
	input.selected {
		background: rgb(50, 50, 50);
	}
</style>
