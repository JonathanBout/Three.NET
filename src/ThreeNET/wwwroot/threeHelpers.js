import * as THREE from './node_modules/three/build/three.module.min.js'
export function create(className, ...parameters) {
	if (parameters.length === 1) {
		parameters = parameters[0];
	}
	if (parameters.length > 0) {
		return new THREE[className](...parameters);
	}
	return new THREE[className]();
}

export function replaceDomElement(element, threeRenderer) {
	element.appendChild(threeRenderer.domElement);
}

export function setProperty(object, identifier, value) {
	const splits = identifier.split('.');
	let tempObj = object;
	for (let i = 0; i < splits.length - 1; i++) {
		tempObj = tempObj[splits[i]];
	}
	tempObj[splits.slice(-1)[0]] = value;
}

export async function helperRequestAnimationFrame(dotnetObject, identifier) {
	requestAnimationFrame(async () => {
		await dotnetObject.invokeMethodAsync(identifier);
		console.log('animframe');
	});
}

export function getObjectRef(dotnetObject) {
	return dotnetObject;
}