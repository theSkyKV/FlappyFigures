mergeInto(LibraryManager.library, {

	ShowFigureOpenAdvExtern: function () {
		const objName = 'MainMenuSceneDirector';
		ysdk.adv.showRewardedVideo({
			callbacks: {
				onOpen: () => {
					console.log('Video ad open.');
				},
				onRewarded: () => {
					console.log('Rewarded!');
					gameInstance.SendMessage(objName, 'OnRewardReceived');
				},
				onClose: () => {
					console.log('Video ad closed.');
					gameInstance.SendMessage(objName, 'OnAdvClosedOrFailed');
				},
				onError: (e) => {
					console.log('Error while open video ad:', e);
					gameInstance.SendMessage(objName, 'OnAdvClosedOrFailed');
				}
			}
		})
	},

	ShowResurrectAdvExtern: function () {
		const objName = 'LevelSceneDirector';
		ysdk.adv.showRewardedVideo({
			callbacks: {
				onOpen: () => {
					console.log('Video ad open.');
				},
				onRewarded: () => {
					console.log('Rewarded!');
					gameInstance.SendMessage(objName, 'OnRewardReceived');
				},
				onClose: () => {
					console.log('Video ad closed.');
					gameInstance.SendMessage(objName, 'OnAdvClosedOrFailed');
				},
				onError: (e) => {
					console.log('Error while open video ad:', e);
					gameInstance.SendMessage(objName, 'OnAdvClosedOrFailed');
				}
			}
		})
	},
	
	SaveExtern: function (data) {
		var dataString = UTF8ToString(data);
		var json = JSON.parse(dataString);
		player.setData(json);
	},
	
	LoadExtern: function () {
		if (gameInstance === null)
		{
			console.log('GameInstance is NULL');
			return;
		}

		if (player === null)
		{
			console.log('Player is NULL');
			return;
		}

		player.getData().then(_data => {
			if (_data === null)
			{
				console.log('data is NULL');
				gameInstance.SendMessage('ProjectContext', 'FirstTimeInitDataFromYandex');
				return null;
			}
			const json = JSON.stringify(_data);
			gameInstance.SendMessage('ProjectContext', 'SetDataFromYandex', json);
			console.log('data is set');
		});
	},

});