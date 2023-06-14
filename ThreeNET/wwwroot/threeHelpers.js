import * as THREE from "./threeJS/package/build/three.module.js"

export function create(className, ...parameters) {
	return new THREE[className](...parameters)
}

export function replaceDomElement(element, threeRenderer) {
	element.appendChild(threeRenderer.domElement);
}

export function setProperty(object, identifier, value) {
	var splits = identifier.split('.');
	var tempObj = object;
	for (let i = 0; i < splits.length - 1; i++) {
		tempObj = tempObj[splits[i]];
	}
	tempObj[splits.slice(-1)] = value;
}

const subscribers = [];
export async function helperRequestAnimationFrame(dotnetObject, identifier, subscribeIfNotFound) {
	if (typeof subscribers[dotnetObject] === undefined || subscribers[dotnetObject] === null) {
		if (subscribeIfNotFound) {
			subscribers[dotnetObject] = identifier;
		} else {
			return;
		}
	}
	requestAnimationFrame(async () => {
		await helperRequestAnimationFrame(dotnetObject, identifier, false);
	});
	await dotnetObject.invokeMethodAsync(identifier);
}
export function clearAnimationFrameRequests() {
	subscribers.length = 0;
}