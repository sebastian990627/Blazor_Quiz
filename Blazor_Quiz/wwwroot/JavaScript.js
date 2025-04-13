function observeElementVisibilityById(elementId, dotnetHelper) {
    const element = document.getElementById(elementId);
    if (!element) {
        console.error(`Element o id ${elementId} nie został znaleziony.`);
        return;
    }
    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                dotnetHelper.invokeMethodAsync('elementVisible');
            } else {
                dotnetHelper.invokeMethodAsync('elementNotVisible');
            }
        });
    });
    observer.observe(element);
}




function observeElementAdditionById(elementId) {
    // Funkcja obserwująca widoczność elementu
    const observeVisibility = (element) => {
        const observer = new IntersectionObserver(entries => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    element.focus();
                    //    dotnetHelper.invokeMethodAsync('elementVisible');
                } else {
                    //dotnetHelper.invokeMethodAsync('elementNotVisible');
                }
            });
        });
        observer.observe(element);
    }

    // Utwórz obserwator mutacji
    const mutationObserver = new MutationObserver(mutations => {
        mutations.forEach(mutation => {
            if (mutation.type === 'childList') {
                const addedNode = Array.from(mutation.addedNodes).find(node => node.id === elementId);
                if (addedNode) {
                    observeVisibility(addedNode);
                    mutationObserver.disconnect(); // Odczep obserwatora po znalezieniu elementu
                }
            }
        });
    });

    // Rozpocznij obserwację
    mutationObserver.observe(document.body, {
        childList: true,
        subtree: true
    });
}

function observeElementAdditionByIdMethod(elementId, dotnetHelper) {
    // Funkcja obserwująca widoczność elementu
    const observeVisibility = (element) => {
        const observer = new IntersectionObserver(entries => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    // element.focus();
                    dotnetHelper.invokeMethodAsync('elementVisible');
                } else {
                    dotnetHelper.invokeMethodAsync('elementVisible');
                }
            });
        });
        observer.observe(element);
    }

    // Utwórz obserwator mutacji
    const mutationObserver = new MutationObserver(mutations => {
        mutations.forEach(mutation => {
            if (mutation.type === 'childList') {
                const addedNode = Array.from(mutation.addedNodes).find(node => node.id === elementId);
                if (addedNode) {
                    observeVisibility(addedNode);
                    mutationObserver.disconnect(); // Odczep obserwatora po znalezieniu elementu
                }
            }
        });
    });

    // Rozpocznij obserwację
    mutationObserver.observe(document.body, {
        childList: true,
        subtree: true
    });
}



window.startIntersectionObserverForAll = (dotNetHelper) => {
    let elements = document.querySelectorAll('.none-visible');

    let observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.remove('none-visible');
                entry.target.classList.add('animation-effect-2');
                dotNetHelper.invokeMethodAsync('OnElementInView');
            }
        });
    });

    elements.forEach(element => observer.observe(element));

    // Optional: For dynamically added elements, observe changes in the DOM
    let mutationObserver = new MutationObserver(() => {
        let newElements = document.querySelectorAll('.none-visible');
        newElements.forEach(element => observer.observe(element));
    });

    mutationObserver.observe(document.body, { childList: true, subtree: true });
};